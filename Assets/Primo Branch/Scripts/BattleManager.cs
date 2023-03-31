using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Collections;
using TMPro;

public class BattleManager : MonoBehaviour
{
    // Variables

    public static BattleManager Instance;
    public AudioSource audio;
    public AudioClip tempAttackFX;
    public int enemyCount;
    public List<StatSheet> combatants = new List<StatSheet>();
    public GameObject initBar;
    public string encounterScene;
    public AudioClip battleMusic;

    public bool tickInitiative;
    public bool inBattle;
    public StatSheet currentCombatant;
    public StatSheet currentTarget;
    public Image actionMenu;
    public GameObject targetIcon;
    public GameObject currentTargetIcon;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        actionMenu.gameObject.SetActive(false);
        Debug.Log("The name of this Scene is " + SceneManager.GetActiveScene().name + "!");
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            EncounterStart encounter = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EncounterStart>();
            Debug.Log("The current EncounterStart script is " + encounter + "!");
            Debug.Log(SceneManager.GetActiveScene().name + " loaded!");
            foreach (StatSheet enemy in encounter.enemyStats)
            {
                AddCombatant(enemy);
            }
            if (enemyCount == 1)
            {
                GameObject.Find("EnemyHP 1").transform.Translate(Vector2.down * 60);
                GameObject.Find("EnemyHP 2").SetActive(false);
                GameObject.Find("EnemyHP 3").SetActive(false);
            }
            else if (enemyCount == 2)
            {
                GameObject.Find("EnemyHP 1").transform.Translate(Vector2.down * 30);
                GameObject.Find("EnemyHP 2").transform.Translate(Vector2.down * 30);
                GameObject.Find("EnemyHP 3").SetActive(false);
            }
            //BattleManager.Instance.AddCombatant(collision.gameObject.GetComponent<StatSheet>());
            encounterScene = encounter.encounterScene;
            battleMusic = encounter.battleMusic;
            audio.clip = battleMusic;
            audio.Play(0);
            audio.loop = true;
            tickInitiative = true;
            Instance.inBattle = true;
            Destroy(encounter.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tickInitiative && inBattle)
        {
            float baseInit = 0;
            float initCount = 0;
            foreach(StatSheet combatant in combatants)
            {
                baseInit += combatant.speed;
                initCount++;
            }
            baseInit /= initCount;
            foreach(StatSheet combatant in combatants)
            {
                combatant.initiative += combatant.speed / baseInit * 35 * Time.deltaTime;
                combatant.initBar.GetComponentInChildren<initiativeBar>().updateInitiativeBar(combatant.initiative);
                if(combatant.initiative >= 100)
                {
                    tickInitiative = false;
                    StartTurn(combatant);
                }
            }
        }
    }

    public void AddCombatant(StatSheet combatant)
    {
        if (combatant.isEnemy)
        {
            GameObject character = Instantiate(combatant.character, GameObject.Find("Enemy Spawn " + (enemyCount + 1)).transform.position, transform.rotation);
            StatSheet characterStats = character.GetComponent<StatSheet>();
            enemyCount++;
            combatants.Add(characterStats);
            characterStats.hpBar = GameObject.Find("EnemyHP " + (enemyCount));
            characterStats.hpBar.GetComponent<HpBar>().setMaxHealth(characterStats.maxHp);
            characterStats.hpBar.GetComponent<HpBar>().setHealth(characterStats.hp);
            characterStats.hpBar.transform.Find("Name Text").GetComponent<TextMeshProUGUI>().text = characterStats.characterName;
            characterStats.initBar = Instantiate(initBar, new Vector2(character.transform.position.x, character.transform.position.y + character.GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.5f), transform.rotation);
            Debug.Log(characterStats.name + "'s HP bar is " + characterStats.hpBar.name + "!");
        }
        else
        {
            GameObject character = Instantiate(combatant.character, GameObject.Find("Player Spawn").transform.position, transform.rotation);
            //character.AddComponent<StatSheet>(combatant)
            StatSheet characterStats = character.GetComponent<StatSheet>();
            combatants.Add(characterStats);
            characterStats.hpBar = GameObject.Find("PlayerHP");
            characterStats.hpBar.GetComponent<HpBar>().setMaxHealth(characterStats.maxHp);
            characterStats.hpBar.GetComponent<HpBar>().setHealth(characterStats.hp);
            characterStats.hpBar.transform.Find("Name Text").GetComponent<TextMeshProUGUI>().text = characterStats.characterName;
            characterStats.initBar = Instantiate(initBar, new Vector2(character.transform.position.x, character.transform.position.y + character.GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.5f), transform.rotation);
            characterStats.spMeter = GameObject.Find("SpBar");
            characterStats.spMeter.GetComponent<SpBar>().SetMaxSp(characterStats.maxSp);
            characterStats.spMeter.GetComponent<SpBar>().updateSpBar(characterStats.sp);
            Debug.Log(characterStats.name + "'s HP bar is " + characterStats.hpBar.name + "!");
        }
    }
    
    public void RemoveCombatant(StatSheet combatant)
    {
        combatants.Remove(combatant);
        Destroy(combatant.gameObject);
    }

    public void StartTurn(StatSheet combatant)
    {
        currentCombatant = combatant;
        if (!combatant.isEnemy)
        {
            actionMenu.gameObject.SetActive(true);
            List<StatSheet> enemies = new List<StatSheet>();
            foreach (StatSheet target in combatants)
            {
                if (target.isEnemy)
                {
                    enemies.Add(target);
                }
            }
            SelectTarget(enemies[0]);
        }
        else
        {
            List<StatSheet> players = new List<StatSheet>();
            foreach (StatSheet target in combatants)
            {
                if (!target.isEnemy)
                {
                    players.Add(target);
                }
            }
            currentTarget = players[Random.Range(0, players.Count)];
            BasicAttack();
        }
    }

    public void SelectTarget(StatSheet target)
    {
        if (currentTargetIcon != null)
        {
            Destroy(currentTargetIcon);
        }
        currentTargetIcon = Instantiate(targetIcon, new Vector2(target.transform.position.x, target.transform.position.y - target.GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.1f) , targetIcon.transform.rotation);
        currentTarget = target;
    }

    public void BasicAttack()
    {
        actionMenu.gameObject.SetActive(false);
        audio.PlayOneShot(tempAttackFX);
        float accCheck = Random.Range(0, currentCombatant.accuracy + 1);
        if (accCheck > currentCombatant.evasion)
        {
            float damageDealt = Mathf.Round(currentCombatant.offense * (100 / (100 + currentTarget.armor)));
            Debug.Log("Dealing " + damageDealt + " damage to " + currentTarget.characterName + "!");
            currentTarget.hp -= Mathf.Round(currentCombatant.offense * (100 / (100 + currentTarget.armor)));
            currentTarget.hpBar.GetComponent<HpBar>().setHealth(currentTarget.hp);
            if (currentTarget.hp <= 0)
            {
                currentTarget.hp = 0;
                currentTarget.hpBar.transform.Find("Name Text").GetComponent<TextMeshProUGUI>().text = "**DEAD**";
                RemoveCombatant(currentTarget);
                int enemyCount = 0;
                foreach (StatSheet combatant in combatants)
                {
                    if (combatant.isEnemy)
                    {
                        enemyCount++;
                    }
                }
                if (enemyCount == 0)
                {
                    inBattle = false;
                    SceneManager.LoadScene("" + encounterScene);
                }
            }
        }
        else
        {
            // Play miss sound
            Debug.Log("Wow your aim sukcs");
        }
        currentCombatant.initiative = 0;
        tickInitiative = true;
    }

    public IEnumerator DealDamage(float damageMod, string damageType, bool isAOE)
    {
        yield return new WaitForSeconds(0);
        Debug.Log("Attempting to deal x" + damageMod + " " + damageType + " damage to " + currentCombatant + "!");
        // Deals damage to all enemies
        if (isAOE)
        {
            foreach (StatSheet combatant in combatants)
            {
                if (combatant.isEnemy)
                {
                    if (damageType == "Physical")
                    {
                        float damageDealt = Mathf.Round(currentCombatant.offense * (100 / (100 + currentTarget.armor)) * damageMod);
                        Debug.Log("Dealing " + damageDealt + " Physical damage to " + currentTarget.characterName + "!");
                        combatant.hp -= damageDealt;
                        combatant.hpBar.GetComponent<HpBar>().setHealth(combatant.hp);
                    }
                    else if (damageType == "Magical")
                    {
                        float damageDealt = Mathf.Round(currentCombatant.magic * (100 / (100 + currentTarget.ward)) * damageMod);
                        Debug.Log("Dealing " + damageDealt + " Magical damage to " + currentTarget.characterName + "!");
                        combatant.hp -= damageDealt;
                        combatant.hpBar.GetComponent<HpBar>().setHealth(combatant.hp);
                    }
                }
            }
        }
        else
        // Deals damage to the currently selected target
        {
            if (damageType == "Physical")
            {
                float damageDealt = Mathf.Round(currentCombatant.offense * (100 / (100 + currentTarget.armor)) * damageMod);
                Debug.Log("Dealing " + damageDealt + " Physical damage to " + currentTarget.characterName + "!");
                currentTarget.hp -= damageDealt;
                currentTarget.hpBar.GetComponent<HpBar>().setHealth(currentTarget.hp);
            }
            else if (damageType == "Magical")
            {
                float damageDealt = Mathf.Round(currentCombatant.magic * (100 / (100 + currentTarget.ward)) * damageMod);
                Debug.Log("Dealing " + damageDealt + " Magical damage to " + currentTarget.characterName + "!");
                currentTarget.hp -= damageDealt;
                currentTarget.hpBar.GetComponent<HpBar>().setHealth(currentTarget.hp);
            }
        }
        // Check for enemy death
        if (currentTarget.hp <= 0)
        {
            currentTarget.hp = 0;
            currentTarget.hpBar.transform.Find("Name Text").GetComponent<TextMeshProUGUI>().text = "**DEAD**";
            RemoveCombatant(currentTarget);
            // Check for victory
            int enemyCount = 0;
            foreach (StatSheet combatant in combatants)
            {
                if (combatant.isEnemy)
                {
                    enemyCount++;
                }
            }
            // Return player to overworld (move to another method to allow for victory results later on)
            if (enemyCount == 0)
            {
                inBattle = false;
                SceneManager.LoadScene("" + encounterScene);
            }
        }
        StopCoroutine(DealDamage(damageMod, damageType, isAOE));
    }

    public IEnumerator PlayAnimation(string animation, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        currentCombatant.GetComponent<Animator>().SetTrigger(animation);
        StopCoroutine(PlayAnimation(animation, delayTime));
    }

    public IEnumerator PlaySound(AudioClip sound, int repeats, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        for (int i = 0; i == repeats + 1; i++)
        {
            audio.PlayOneShot(sound);
        }
        StopCoroutine(PlaySound(sound, repeats, delayTime));
    }

    public void Restore(string scaleType, string healType, float healMod, StatSheet target)
    {
        if (healType == "Health")
        {
            if (scaleType == "Magic")
            {
                float healthRestored = currentCombatant.magic * healMod;
                Debug.Log(currentCombatant.name + " is restoring " + target.name + "'s HP by " + healthRestored + " based on " + scaleType + "!");
                target.hp = healthRestored;
            }
            else if (scaleType == "HP")
            {
                float healthRestored = target.maxHp * healMod;
                Debug.Log(currentCombatant.name + " is restoring " + target.name + "'s HP by " + healthRestored + " based on " + scaleType + "!");
                target.hp = healthRestored;
            }
        }
        else if (healType == "Spirit")
        {
            if (scaleType == "Magic")
            {
                float healthRestored = currentCombatant.magic * healMod;
                Debug.Log(currentCombatant.name + " is restoring " + target.name + "'s HP by " + healthRestored + " based on " + scaleType + "!");
                target.sp = healthRestored;
            }
            else if (scaleType == "SP")
            {
                float healthRestored = target.maxSp * healMod;
                Debug.Log(currentCombatant.name + " is restoring " + target.name + "'s HP by " + healthRestored + " based on " + scaleType + "!");
                target.sp = healthRestored;
            }
        }
    }

    public void UseSkill(Skill skill)
    {
        Debug.Log("Using " + skill.skillName + "!");
        for (int i = 0; i < skill.skillSequence.Count; i++)
        {
            Debug.Log("Skill Sequence Index: " + i);
            StartCoroutine(skill.skillSequence[i]);
            Debug.Log("Invoking " + skill.skillSequence[i]);
        }
        currentCombatant.initiative = 0;
        tickInitiative = true;
    }

    Component CopyComponent(Component original, GameObject destination)
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy;
    }
}
