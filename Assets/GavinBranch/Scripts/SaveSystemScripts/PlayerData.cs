using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerData
{
    //public GameObject character;
    public string characterName;


    [Header("Level")]
    public float level = 1;
    public float levelForXpGain = 0;
    public float exp = 0;
    public float expCap = 100;

    [Header("Stats")]
    public float strength;
    public float dexterity;
    public float soul;
    public float guts;
    public float focus;
    public float agility;

    [Header("Location")]
    public float[] position;
    public int scene;

    [Header("Coins")]
    public int coins;

    public int numbOfEnemyKilled;
    public int numbOfCoinsClaimed;

    public PlayerData (StatSheet player)
    {

        coins = CoinsController.coinAmount;

        characterName = player.characterName;

        levelForXpGain = XpGain.levelsGained;
        level = player.level;
        exp = player.exp;
        expCap = player.expCap;

        strength = player.strength;
        dexterity = player.dexterity;
        soul = player.soul;
        guts = player.guts;
        focus = player.focus;
        agility = player.agility;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        scene = SceneManager.GetActiveScene().buildIndex;

        numbOfEnemyKilled = XpGain.NumberOfEnemiesKilled;
        numbOfCoinsClaimed = XpGain.NumberOfCoinsClaimed;

    }
}
