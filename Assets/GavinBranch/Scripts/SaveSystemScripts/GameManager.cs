using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string fileName;
    public StatSheet stat;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(GameObject.Find("Player").GetComponent<StatSheet>(),fileName);
    }

    public void loadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer(fileName);

        stat.character = data.character;
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

    }
}
