using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSheet : MonoBehaviour
{
    // Variables

    public GameObject character;
    public GameObject hpBar;
    public bool isEnemy;
    public string characterName;
    public float initiative = 0;

    public float level = 1;
    public float exp = 0;
    public float expCap = 100;
    public float expYield = 0;

    public float strength;  // Effectiveness of weapons & Max HP
    public float dexterity; // Quickens physical skill cooldowns & enhances accuracy
    public float soul;      // Effectiveness of spells & Magic defense
    public float guts;      // Physical defense, & Critical damage
    public float focus;     // Critical chance & Max SP
    public float agility;   // Initiative & evasion

    public float maxhp;
    public float maxsp;
    public float hp;        // Health points. Run out, you die.
    public float sp;        // Spirit points. Used to cast spells.
    public float offense;   // Used to determine physical damage.
    public float magic;     // Used to determine magical damage & healing.
    public float armor;     // Reduces physical damage taken.
    public float ward;      // Reduces magical damage taken.
    public float speed;     // Determines when you get to take your turn.

    public float accuracy;  // Chance to hit or miss the target you attack.
    public float evasion;   // Chance to avoid incoming attacks.
    public float crit;      // Chance to deal bonus damage. Usually 5%.
    public float punish;    // The amount of bonus damage you deal on a crit. Usually +50% of the original damage.

    // Update is called once per frame
    void Start()
    {
        maxhp = 60 * (1 + (strength / 100 * level));
        hp = maxhp;
        maxsp = 30 * (1 + (focus / 100 * level));
        sp = maxsp;
        offense = 10 + (0.5f * strength * (level / 3));
        magic = 10 + (0.5f * soul * (level / 3));
        armor = 5 + (0.5f * guts * (level / 3));
        ward = 5 + (0.5f * soul * (level / 3));
        speed = 10 * (1 + (agility / 100 * level));

        accuracy = 100 * (1 + (dexterity / 100 * level));
        evasion = 10 * (1 + (agility / 100 * level));
        crit = 10 * (1 + (agility / 100 * level));
        punish = 50 * (1 + (focus / 100 * level));
    }

    public void refreshStat(string stat)
    {

    }
}
