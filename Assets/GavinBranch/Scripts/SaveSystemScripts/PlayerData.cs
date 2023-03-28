using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    //public GameObject character;
    public string characterName;


    [Header("Level")]
    public float level = 1;
    public float exp = 0;
    public float expCap = 100;

    [Header("Stats")]
    public float strength;
    public float dexterity;
    public float soul;
    public float guts;
    public float focus;
    public float agility;

    public float test;

    public PlayerData (StatSheet player)
    {
        //character = player.character;
        characterName = player.characterName;

        level = player.level;
        exp = player.exp;
        expCap = player.expCap;

        strength = player.strength;
        dexterity = player.dexterity;
        soul = player.soul;
        guts = player.guts;
        focus = player.focus;
        agility = player.agility;
    }
}
