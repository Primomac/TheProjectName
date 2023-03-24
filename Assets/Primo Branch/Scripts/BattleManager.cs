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
    public int enemyCount;
    public List<StatSheet> combatants = new List<StatSheet>();
    public string encounterScene;

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
            //BattleManager.Instance.AddCombatant(collision.gameObject.GetComponent<StatSheet>());
            encounterScene = encounter.encounterScene;
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
                combatant.initiative += combatant.speed / baseInit * 20 * Time.deltaTime;
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
            characterStats.hpBar = GameObject.Find("EnemyHP");
            characterStats.hpBar.GetComponent<HpBar>().setMaxHealth(characterStats.maxHp);
            characterStats.hpBar.GetComponent<HpBar>().setHealth(characterStats.hp);
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
        // Play attack sound & animation
        float accCheck = Random.Range(0, currentCombatant.accuracy + 1);
        if (accCheck > currentCombatant.evasion)
        {
            float damageDealt = Mathf.Round(currentCombatant.offense * (100 / (100 + currentTarget.armor)));
            Debug.Log("Dealing " + damageDealt + " damage to " + currentTarget.name + "!");
            currentTarget.hp -= Mathf.Round(currentCombatant.offense * (100 / (100 + currentTarget.armor)));
            currentTarget.hpBar.GetComponent<HpBar>().setHealth(currentTarget.hp);
            if (currentTarget.hp <= 0)
            {
                currentTarget.hp = 0;
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

    private void OnMouseDown()
    {
        SelectTarget(gameObject.GetComponent<StatSheet>());
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
