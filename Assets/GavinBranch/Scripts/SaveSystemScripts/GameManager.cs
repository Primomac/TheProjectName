using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public string fileName;
    public string AutoSave;
    public StatSheet stat;

    private void Awake()
    {
        if(GameObject.Find("player") != null)
        {
            loadPlayer();
        }
    }

    public void SavePlayer()
    {
        stat = GameObject.Find("Player").GetComponent<StatSheet>();
        SaveSystem.SavePlayer(stat,fileName);
    }

    public void loadPlayer()
    {
        stat = GameObject.Find("Player").GetComponent<StatSheet>();

        PlayerData data = SaveSystem.LoadPlayer(fileName);

        //stat.character = data.character;
        stat.characterName = data.characterName;

        XpGain.levelsGained = (int)data.levelForXpGain;
        stat.level = data.level;
        stat.exp = data.exp;
        stat.expCap = data.expCap;

        stat.strength = data.strength;
        stat.dexterity = data.dexterity;
        stat.soul = data.soul;
        stat.guts = data.guts;
        stat.focus = data.focus;
        stat.agility = data.agility;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        stat.transform.position = position;

        CoinsController.coinAmount = data.coins;
        Debug.Log("Coins: " + CoinsController.coinAmount);

        XpGain.NumberOfEnemiesKilled = data.numbOfEnemyKilled;
        XpGain.NumberOfCoinsClaimed = data.numbOfCoinsClaimed;

        XpGain.Xpleft = (int)data.exp;
        XpGain.XpCap = data.expCap;
    }



    //auto save
    public void AutoSavePlayer()
    {
        SaveSystem.SavePlayer(stat, AutoSave);
    }

    public void AutoloadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer(AutoSave);
        SceneManager.LoadScene(data.scene);
    }
    private void OnApplicationQuit()
    {
        if(stat == null)
        {
            stat = GameObject.Find("Player").AddComponent<StatSheet>();
            SaveSystem.SavePlayer(stat, fileName);
        }

        if(stat != null)
        {
            SavePlayer();
        }

    }

    /*
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "VictoryScene")
        {
            PlayerData data = SaveSystem.LoadPlayer(fileName);

            //stat.character = data.character;
            stat.characterName = data.characterName;

            stat.level = data.level;
            stat.exp = data.exp;
            stat.expCap = data.expCap;

            stat.strength = data.strength;
            stat.dexterity = data.dexterity;
            stat.soul = data.soul;
            stat.guts = data.guts;
            stat.focus = data.focus;
            stat.agility = data.agility;

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            stat.transform.position = position;

            CoinsController.coinAmount = data.coins;
        }
    }
    */






}
