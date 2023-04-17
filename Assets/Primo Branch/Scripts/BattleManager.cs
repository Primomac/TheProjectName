using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Collections;
using TMPro;
using System;

public class BattleManager : MonoBehaviour
{
    // Variables

    public static BattleManager Instance;
    public AudioSource audio;
    public AudioClip no;
    public int enemyCount;
    public List<StatSheet> combatants = new List<StatSheet>();
    public GameObject initBar;
    public GameObject statStorage;
    public string encounterScene;
    public AudioClip battleMusic;

    public bool tickInitiative;
    public bool inBattle;
    public bool menuDown;
    public GameObject currentMenu;
    public StatSheet currentCombatant;
    public StatSheet currentTarget;
    public GameObject actionMenu;
    public GameObject targetIcon;
    public GameObject currentTargetIcon;

    // Awake is called when the object is loaded for the first time
    void Awake()
    {
        Instance = this;
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

        List<int> PeopleToKill = new List<int>();
        foreach(StatSheet combatant in combatants)
        {
            // People die when they are killed
            if (combatant.hp <= 0)
            {
                combatant.hp = 0;
                combatant.hpBar.transform.Find("Name Text").GetComponent<TextMeshProUGUI>().text = "**DEAD**";
                //RemoveCombatant(combatant);

                PeopleToKill.Add(combatants.IndexOf(combatant));

                foreach (StatSheet player in combatants)
                {
                    if (!player.isEnemy)
                    {
                        player.exp += combatant.expYield;
                    }
                }
            }
            // Check for victory/defeat
            int enemyCount = 0;
            bool playerLives = false;
            foreach (StatSheet enemy in combatants)
            {
                if (enemy.isEnemy)
                {
                    enemyCount++;
                }
                else
                {
                    playerLives = true;
                }
            }
            // Return player to overworld (move to another method to allow for victory results later on)
            if (!playerLives)
            {
                Debug.Log("YOU DIED");
                Application.Quit();
            }
            else if (enemyCount == 0)
            {
                inBattle = false;
                GameObject storage = Instantiate(statStorage, transform.position, transform.rotation);
                storage.GetComponent<Storage>().currentStats = combatants[0];
                Debug.Log("Current EXP gainer is " + combatants[0]);
                Debug.Log("Current EXP (BattleManager) is " + storage.GetComponent<Storage>().currentStats.exp);
                SceneManager.LoadScene("" + encounterScene);
            }
        }
        foreach (int enemy in PeopleToKill)
        {
            Destroy(combatants[enemy].initBar);
            RemoveCombatant(combatants[enemy]);
        }
    }

    public void AddCombatant(StatSheet combatant) // Adds a combatant to the battle scene
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
            StatSheet characterStats = character.GetComponent<StatSheet>();
            combatant.UpdateStats(characterStats);
            combatants.Add(characterStats);
            Debug.Log("Player's level is " + combatant.level + ", which should be equivalent to " + characterStats.level);
            characterStats.hpBar = GameObject.Find("PlayerHP");
            characterStats.hpBar.GetComponent<HpBar>().setMaxHealth(characterStats.maxHp);
            characterStats.hpBar.GetComponent<HpBar>().setHealth(characterStats.hp);
            characterStats.hpBar.transform.Find("Name Text").GetComponent<TextMeshProUGUI>().text = characterStats.characterName;
            characterStats.initBar = Instantiate(initBar, new Vector2(character.transform.position.x, character.transform.position.y + character.GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.5f), transform.rotation);
            characterStats.spMeter = GameObject.Find("SpBar");
            characterStats.spMeter.GetComponent<SpBar>().SetMaxSp(characterStats.maxSp);
            characterStats.spMeter.GetComponent<SpBar>().updateSpBar(characterStats.sp);
            Debug.Log(characterStats.name + "'s HP bar is " + characterStats.hpBar.name + "!");
            Debug.Log(characterStats.name + "'s SP bar is " + characterStats.spMeter.name + "!");
        }
    }

    public void RemoveCombatant(StatSheet combatant) // Removes a combatant from the battle scene
    {
        combatants.Remove(combatant);
        Destroy(combatant.gameObject);
    }

    public void StartTurn(StatSheet combatant) // Occurs whenever a target's initiative reaches 100
    {
        currentCombatant = combatant;
        if (!combatant.isEnemy)
        {
            MenuSwap(actionMenu);
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
            currentTarget = players[UnityEngine.Random.Range(0, players.Count)];
            UseSkill(currentTarget.skillList[UnityEngine.Random.Range(0, currentTarget.skillList.Count)]);  
        }
    }

    public void SelectTarget(StatSheet target) // Changes the target of the next skill
    {
        if (currentTargetIcon != null)
        {
            Destroy(currentTargetIcon);
        }
        currentTargetIcon = Instantiate(targetIcon, new Vector2(target.transform.position.x, target.transform.position.y - target.GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.1f) , targetIcon.transform.rotation);
        currentTarget = target;
    }

    public void MenuSwap(GameObject menu) // Swaps the current turn menu to a different one
    {
        Debug.Log("Starting Coroutine and changing menu to " + menu);
        StartCoroutine(ActualMenuSwap(menu));
    }

    public IEnumerator ActualMenuSwap(GameObject menu) // Actually swaps the current turn menu to a different one
    {
        yield return new WaitForSeconds(0);
        Debug.Log("Menu swapping to " + menu);
        if (!menuDown && currentMenu != null)
        {
            Debug.Log("Moving " + currentMenu);
            while (currentMenu.transform.position.y > -54)
            {
                currentMenu.transform.Translate(Vector2.down * Time.deltaTime * 432);
                yield return new WaitForEndOfFrame();
            }
            if (currentMenu.transform.position.y <= -54)
            {
                currentMenu.transform.position = new Vector2(384, -54);
                menuDown = true;
            }
        }
        else
        {
            menuDown = true;
        }
        if (menuDown)
        {
            Debug.Log("CurrentMenu has been shifted down or does not exist!");
            if (menu != null)
            {
                Debug.Log("Moving " + menu + "from " + menu.transform.position.y);
                while (menu.transform.position.y < 54)
                {
                    menu.transform.Translate(Vector2.up * Time.deltaTime * 432);
                    yield return new WaitForEndOfFrame();
                }
                if (menu.transform.position.y >= 54)
                {
                    menu.transform.position = new Vector2(384, 54);
                    currentMenu = menu;
                    menuDown = false;
                    Debug.Log("Menu movement has finished!");
                    StopCoroutine(ActualMenuSwap(menu));
                }
            }
            else
            {
                menuDown = false;
                Debug.Log("Menu does not exist!");
                StopCoroutine(ActualMenuSwap(menu));
            }
        }
    }

    public IEnumerator DealDamage(float damageMod, string damageType, bool isAOE) // Deals damage to the currently selected target (or all targets)
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
                    float accCheck = UnityEngine.Random.Range(0, currentCombatant.accuracy + 1);
                    if (accCheck > currentTarget.evasion)
                    {
                        if (damageType == " Physical")
                        {
                            float damageDealt = Mathf.Round(currentCombatant.offense * (100 / (100 + combatant.armor)) * damageMod);
                            Debug.Log("Dealing " + damageDealt + " Physical damage to " + combatant.characterName + "!");
                            combatant.hp -= damageDealt;
                            combatant.hpBar.GetComponent<HpBar>().setHealth(combatant.hp);
                        }
                        else if (damageType == " Magical")
                        {
                            float damageDealt = Mathf.Round(currentCombatant.magic * (100 / (100 + combatant.ward)) * damageMod);
                            Debug.Log("Dealing " + damageDealt + " Magical damage to " + combatant.characterName + "!");
                            combatant.hp -= damageDealt;
                            combatant.hpBar.GetComponent<HpBar>().setHealth(combatant.hp);
                        }
                    }
                    else
                    {
                        // Play miss sound
                        Debug.Log("Wow your aim sukcs");
                    }
                }
            }
        }
        else
        // Deals damage to the currently selected target
        {
            float accCheck = UnityEngine.Random.Range(0, currentCombatant.accuracy + 1);
            if (accCheck > currentTarget.evasion)
            {
                if (damageType == " Physical")
                {
                    float damageDealt = Mathf.Round(currentCombatant.offense * (100 / (100 + currentTarget.armor)) * damageMod);
                    Debug.Log("Dealing " + damageDealt + " Physical damage to " + currentTarget.characterName + "!");
                    currentTarget.hp -= damageDealt;
                    currentTarget.hpBar.GetComponent<HpBar>().setHealth(currentTarget.hp);
                }
                else if (damageType == " Magical")
                {
                    float damageDealt = Mathf.Round(currentCombatant.magic * (100 / (100 + currentTarget.ward)) * damageMod);
                    Debug.Log("Dealing " + damageDealt + " Magical damage to " + currentTarget.characterName + "!");
                    currentTarget.hp -= damageDealt;
                    currentTarget.hpBar.GetComponent<HpBar>().setHealth(currentTarget.hp);
                }
            }
            else
            {
                // Play miss sound
                Debug.Log("Wow your aim sukcs");
            }
        }
        StopCoroutine(DealDamage(damageMod, damageType, isAOE));
    }

    public IEnumerator PlayAnimation(string animation) // Plays an animation from the current skill user
    {
        yield return new WaitForSeconds(0);
        currentCombatant.GetComponent<Animator>().SetTrigger(animation);
        StopCoroutine(PlayAnimation(animation));
    }

    public IEnumerator PlaySound(AudioClip sound, int repeats, float delayTime) // Plays a sound
    {
        yield return new WaitForSeconds(delayTime);
        Debug.Log("Sound is " + sound.name + "!");
        for (int i = 0; i == repeats + 1; i++)
        {
            audio.PlayOneShot(sound);
        }
        StopCoroutine(PlaySound(sound, repeats, delayTime));
    }

    public IEnumerator PlayEffect(GameObject effect, bool onSelf, float length) // Plays an animation over the target
    {
        StatSheet target;
        if (onSelf) { target = currentCombatant; } else { target = currentTarget; }
        GameObject effectInst = Instantiate(effect, target.transform.position, transform.rotation);
        yield return new WaitForSeconds(length);
        Destroy(effectInst);
    }

    public IEnumerator Restore(string scaleType, string healType, float healMod, StatSheet target) // Restores HP or SP to a combatant
    {
        yield return new WaitForSeconds(0);
        if (healType == " Health")
        {
            Debug.Log("Restoring Health!");
            if (scaleType == "Magic")
            {
                float healthRestored = currentCombatant.magic * healMod;
                Debug.Log(currentCombatant.name + " is restoring " + target.name + "'s HP by " + healthRestored + " based on " + scaleType + "!");
                target.hp += Mathf.Round(healthRestored);
            }
            else if (scaleType == "HP")
            {
                float healthRestored = target.maxHp * healMod;
                Debug.Log(currentCombatant.name + " is restoring " + target.name + "'s HP by " + healthRestored + " based on " + scaleType + "!");
                target.hp += Mathf.Round(healthRestored);
            }
            target.hpBar.GetComponent<HpBar>().setHealth(target.hp);
        }
        else if (healType == " Spirit")
        {
            Debug.Log("Restoring Spirit!");
            if (scaleType == "Magic")
            {
                float healthRestored = currentCombatant.magic * healMod;
                Debug.Log(currentCombatant.name + " is restoring " + target.name + "'s HP by " + healthRestored + " based on " + scaleType + "!");
                target.sp += Mathf.Round(healthRestored);
            }
            else if (scaleType == "SP")
            {
                float healthRestored = target.maxSp * healMod;
                Debug.Log(currentCombatant.name + " is restoring " + target.name + "'s HP by " + healthRestored + " based on " + scaleType + "!");
                target.sp += Mathf.Round(healthRestored);
            }
            target.spMeter.GetComponent<SpBar>().updateSpBar(target.hp);
        }
        StopCoroutine(Restore(scaleType, healType, healMod, target));
    }

    public void UseSkill(Skill skill) // Uses a skill and performs its methods in order
    {
        // Check to see if SP cost is met
        if (skill.spCost > currentCombatant.sp)
        {
            audio.PlayOneShot(no);
            return;
        }
        currentCombatant.sp -= skill.spCost;
        if (!currentCombatant.isEnemy)
        {
            currentCombatant.spMeter.GetComponent<SpBar>().updateSpBar(currentCombatant.sp);
        }
        MenuSwap(null);
        Debug.Log("Using " + skill.skillName + "!");
        for (int i = 0; i < skill.skillSequence.Count; i++)
        {
            Debug.Log("Skill Sequence Index: " + i);
            string[] args = skill.skillSequence[i].Split('(', ',', ')');
            string coroutineName = args[0];
            MethodInfo coroutineMethod = typeof(BattleManager).GetMethod(coroutineName);
            if (args[0].Equals("PlaySound"))
            {
                StartCoroutine((IEnumerator)coroutineMethod.Invoke(this, new object[] { skill.sfx[int.Parse(args[1])], int.Parse(args[2]), float.Parse(args[3]) }));
            }
            else if (args[0].Equals("PlayAnimation"))
            {
                StartCoroutine((IEnumerator)coroutineMethod.Invoke(this, new object[] { args[1] }));
            }
            else if (args[0].Equals("PlayEffect"))
            {
                StartCoroutine((IEnumerator)coroutineMethod.Invoke(this, new object[] { skill.effects[int.Parse(args[1])], bool.Parse(args[2]), float.Parse(args[3]) }));
            }
            else if (args[0].Equals("DealDamage"))
            {
                StartCoroutine((IEnumerator)coroutineMethod.Invoke(this, new object[] { float.Parse(args[1]), args[2], bool.Parse(args[3]) }));
                Debug.Log("Dealing " + args[2] + " damage!");
            }
            else if (args[0].Equals("Restore"))
            {
                StatSheet input = currentCombatant;
                if (!args[4].Equals(" currentCombatant"))
                {
                    input = currentTarget;
                }
                StartCoroutine((IEnumerator)coroutineMethod.Invoke(this, new object[] { args[1], args[2], float.Parse(args[3]), input }));
            }
            Debug.Log("Invoking " + skill.skillSequence[i]);
        }
        currentCombatant.initiative = 0;
        tickInitiative = true;
    }
}
