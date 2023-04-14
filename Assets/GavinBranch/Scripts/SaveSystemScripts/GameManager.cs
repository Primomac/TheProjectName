using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        addEXP();
    }


    public string fileName;
    public StatSheet stat;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(stat,fileName);
    }

    public void loadPlayer()
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

    public void addEXP()
    {
        //stat.level = stat.level + XpGain.levelsGained;
        //stat.exp = stat.exp + XpGain.Xpleft;
    }
}
