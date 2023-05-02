using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EquipmentStatStorage : MonoBehaviour
{
    public float[] EquipmentStatsArray;
    public float[] StatsForText;
    public float[] baseStats;
    public GameObject EM;
    public StatSheet player;
    public TextMeshProUGUI pointText;

    public static EquipmentStatStorage storageInstance;

    // Start is called before the first frame update

    private void Awake()
    {
        if (storageInstance == null)
        {
            storageInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        player = GameObject.Find("Player").GetComponent<StatSheet>();
    }
    void Start()
    {
        SetBaseStats();
        UpdatePointText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePointText();

        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<StatSheet>();
            changeStats();
        }
    }

    private void UpdatePointText()
    {
        pointText.text = "Points = " + player.statPoints;
    }

    public void changeStats()
    {
        Debug.Log("Stats changed!");
        EM = GameObject.Find("EquipManager");
        // reset stats
        player.strength = baseStats[0];
        player.dexterity = baseStats[1];
        player.soul = baseStats[2];
        player.guts = baseStats[3];
        player.focus = baseStats[4];
        player.agility = baseStats[5];

        // add equipped items' stats
        foreach (Item item in EM.GetComponent<EquipManager>().Items)
        {
            player.strength += item.strengthModification;
            player.dexterity += item.dexterityModification;
            player.soul += item.soulModification;
            player.guts += item.gutsModification;
            player.focus += item.focusModification;
            player.agility += item.agilityModification;
        }
        SetBaseStats();
    }

    public void SetBaseStats()
    {
        Debug.Log("Base stats set!");
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
            StatsForText[0] =item.strengthModification;
            Debug.Log(item.strengthModification);
            Debug.Log(StatsForText[0]);
            StatsForText[1] =item.dexterityModification;
            StatsForText[2] =item.soulModification;
            StatsForText[3] =item.gutsModification;
            StatsForText[4] =item.focusModification;
            StatsForText[5] =item.agilityModification;
        }

        baseStats[0] = player.strength - EquipmentStatsArray[0];
        baseStats[1] = player.dexterity - EquipmentStatsArray[1];
        baseStats[2] = player.soul - EquipmentStatsArray[2];
        baseStats[3] = player.guts - EquipmentStatsArray[3];
        baseStats[4] = player.focus - EquipmentStatsArray[4];
        baseStats[5] = player.agility - EquipmentStatsArray[5];
    }


    //use points to increase stats
    public void StatIncrease(string stat)
    {
        if (player.statPoints >= 1)
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

