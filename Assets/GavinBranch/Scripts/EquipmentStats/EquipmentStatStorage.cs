using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipmentStatStorage : MonoBehaviour
{
    public float[] EquipmentStatsArray;
    public float[] baseStats;
    public GameObject EM;
    public StatSheet player;
    public TextMeshProUGUI pointText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<StatSheet>();
        SetBaseStats();
    }

    // Update is called once per frame
    void Update()
    {
        pointText.text = "Points = "+player.statPoints;
    }
    public void changeStats()
    {
        EquipmentStatsArray[0] = 0;
        EquipmentStatsArray[1] = 0;
        EquipmentStatsArray[2] = 0;
        EquipmentStatsArray[3] = 0;
        EquipmentStatsArray[4] = 0;
        EquipmentStatsArray[5] = 0;
        foreach (Item item in EM.GetComponent<EquipManager>().Items)
        {
            EquipmentStatsArray[0] = EquipmentStatsArray[0] + item.strengthModification;
            EquipmentStatsArray[1] = EquipmentStatsArray[1] + item.dexterityModification;
            EquipmentStatsArray[2] = EquipmentStatsArray[2] + item.soulModification;
            EquipmentStatsArray[3] = EquipmentStatsArray[3] + item.gutsModification;
            EquipmentStatsArray[4] = EquipmentStatsArray[4] + item.focusModification;
            EquipmentStatsArray[5] = EquipmentStatsArray[5] + item.agilityModification;
        }
    }
    public void SetBaseStats()
    {
        baseStats[0] = player.strength;
        baseStats[1] = player.dexterity;
        baseStats[2] = player.soul;
        baseStats[3] = player.guts;
        baseStats[4] = player.focus;
        baseStats[5] = player.agility;
    }
    public void StatIncrease(string stat)
    {
        if(player.statPoints >= 1)
        {
            player.statPoints--;
            switch (stat)
            {
                case "1":
                    player.strength++;
                    break;
                case "2":
                    player.dexterity++;
                    break;
                case "3":
                    player.soul++;
                    break;
                case "4":
                    player.guts++;
                    break;
                case "5":
                    player.focus++;
                    break;
                case "6":
                    player.agility++;
                    break;
            }
                    SetBaseStats();
        }
    }
}
