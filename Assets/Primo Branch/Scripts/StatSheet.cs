using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSheet : MonoBehaviour
{
    // Variables
    [Header("Meta")]
    public GameObject character;
    public GameObject hpBar;
    public GameObject initBar;
    public GameObject spMeter;
    public bool isEnemy;
    public string characterName;
    public float initiative = 0;

    [Header("Leveling")]
    public float level = 1;
    public float exp = 0;
    public float expCap = 100;
    public float expYield = 0;
    public float coinYield = 0;
    public float statPoints = 0;

    [Header("Primary Stats")]
    public float strength;  // Physical DMG & Max HP
    public float dexterity; // Accuracy
    public float soul;      // Magical DMG & Magic Defense
    public float guts;      // Physical Defense, & Critical damage
    public float focus;     // Critical chance & Max SP
    public float agility;   // Initiative & evasion

    [Header("Battle Stats")]
    public float maxHp;
    public float maxSp;
    public float hp;        // Health points. Run out, you die.
    public float sp;        // Spirit points. Used to perform stronger skills.
    public float offense;   // Used to determine physical damage.
    public float magic;     // Used to determine magical damage & healing.
    public float armor;     // Reduces physical damage taken.
    public float ward;      // Reduces magical damage taken.
    public float speed;     // Determines when you get to take your turn.

    [Header("Chance Stats")]
    public float accuracy;  // Chance to hit or miss the target you attack.
    public float evasion;   // Chance to avoid incoming attacks.
    public float crit;      // Chance to deal bonus damage. Usually 5%.
    public float punish;    // The amount of bonus damage you deal on a crit. Usually +50% of the original damage.

    [Header("Skills")]
    public List<Skill> skillList;

    // Update is called once per frame
    void Awake()
    {
        for (int i = 1; i < level; i++)
        {
            expCap += Mathf.Round(expCap * 0.45f);
        }

        maxHp = Mathf.Round(60 * (1 + (strength / 100 * level)));
        hp = maxHp;
        maxSp = Mathf.Round(30 * (1 + (focus / 100 * level)));
        sp = maxSp;
        offense = Mathf.Round(10 + (0.5f * strength * (level / 3)));
        magic = Mathf.Round(10 + (0.5f * soul * (level / 3)));
        armor = Mathf.Round(5 + (0.5f * guts * (level / 3)));
        ward = Mathf.Round(5 + (0.5f * soul * (level / 3)));
        speed = Mathf.Round(10 + (1 * (agility / 10 * level)));

        accuracy = Mathf.Round(100 * (1 + (dexterity / 1000 * level)));
        evasion = Mathf.Round(10 * (1 + (agility / 100 * level)));
        crit = Mathf.Round(10 * (1 + (agility / 1000 * level)));
        punish = Mathf.Round(50 * (1 + (focus / 1000 * level)));
    }

    private void OnMouseDown()
    {
        if (BattleManager.Instance.inBattle)
        {
            BattleManager.Instance.SelectTarget(this);
        }
    }

    public void UpdateStats(StatSheet stats)
    {
        stats.level = level;
        stats.exp = exp;
        stats.expCap = expCap;
        stats.statPoints = statPoints;

        stats.strength = strength;
        stats.dexterity = dexterity;
        stats.soul = soul;
        stats.guts = guts;
        stats.focus = focus;
        stats.agility = agility;

        // Copy skills from the original StatSheet
        stats.skillList = new List<Skill>();
        foreach (Skill skill in skillList)
        {
            stats.skillList.Add(skill);
        }

        // Level up!

        if (stats.exp > stats.expCap)
        {
            for (float xp = stats.exp; xp > stats.expCap; xp -= stats.expCap)
            {
                Debug.Log("Leveling up!");
                stats.level++;
                stats.statPoints += 3;
                for (int i = 0; i < 3; i++)
                {
                    int bonusStat = Random.Range(1, 6);
                    if (bonusStat == 1)
                    {
                        stats.strength++;
                        Debug.Log("Bonus stat is Strength!");
                    }
                    else if (bonusStat == 2)
                    {
                        stats.dexterity++;
                        Debug.Log("Bonus stat is Dexterity!");
                    }
                    else if (bonusStat == 3)
                    {
                        stats.soul++;
                        Debug.Log("Bonus stat is Soul!");
                    }
                    else if (bonusStat == 4)
                    {
                        stats.focus++;
                        Debug.Log("Bonus stat is Focus!");
                    }
                    else
                    {
                        stats.agility++;
                        Debug.Log("Bonus stat is Agility!");
                    }
                }
                stats.exp -= stats.expCap;
                stats.expCap += Mathf.Round(stats.expCap * 0.45f);
            }
        }

        stats.maxHp = Mathf.Round(60 * (1 + (stats.strength / 100 * stats.level)));
        stats.hp = maxHp;
        stats.maxSp = Mathf.Round(30 * (1 + (stats.focus / 100 * stats.level)));
        stats.sp = maxSp;
        stats.offense = Mathf.Round(10 + (0.5f * stats.strength * (stats.level / 3)));
        stats.magic = Mathf.Round(10 + (0.5f * stats.soul * (stats.level / 3)));
        stats.armor = Mathf.Round(5 + (0.5f * stats.guts * (stats.level / 3)));
        stats.ward = Mathf.Round(5 + (0.5f * stats.soul * (stats.level / 3)));
        stats.speed = agility;

        stats.accuracy = Mathf.Round(100 * (1 + (stats.dexterity / 1000 * stats.level)));
        stats.evasion = Mathf.Round(10 * (1 + (stats.agility / 100 * stats.level)));
        stats.crit = Mathf.Round(10 * (1 + (stats.agility / 1000 * stats.level)));
        stats.punish = Mathf.Round(50 * (1 + (stats.focus / 1000 * stats.level)));
        Debug.Log(string.Join(", ", skillList));
    }
}
