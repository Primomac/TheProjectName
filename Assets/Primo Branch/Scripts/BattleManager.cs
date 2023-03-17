using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    // Variables

    public static BattleManager Instance;
    public List<StatSheet> combatants = new List<StatSheet>();
    public Scene encounterScene;

    public bool tickIniative;
    public bool inBattle;
    public StatSheet currentCombatant;
    public StatSheet currentTarget;
    public Image actionMenu;
    public GameObject targetIcon;
    public GameObject currentTargetIcon;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
        actionMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tickIniative == true)
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
                combatant.initiative += combatant.speed / baseInit * 20;
                if(combatant.initiative >= 100)
                {
                    tickIniative = false;
                    StartTurn(combatant);
                }
            }
        }
    }

    public void StartTurn(StatSheet combatant)
    {
        combatant = currentCombatant;
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
    }

    public void SelectTarget(StatSheet target)
    {
        if (currentTargetIcon != null)
        {
            Destroy(currentTargetIcon);
        }
        currentTargetIcon = Instantiate(targetIcon, target.gameObject.transform.position, targetIcon.transform.rotation);
        currentTarget = target;
    }

    public void BasicAttack()
    {
        actionMenu.gameObject.SetActive(false);
        // Play attack sound & animation
        float accCheck = Random.Range(0, currentCombatant.accuracy + 1);
        if (accCheck > currentCombatant.evasion)
        {
            currentTarget.hp -= currentCombatant.offense * (100 / 100 + currentTarget.armor);
            if (currentTarget.hp <= 0)
            {
                currentTarget.hp = 0;
                Invoke("Destroy(currentTarget.gameObject)", 1);
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
    }
}
