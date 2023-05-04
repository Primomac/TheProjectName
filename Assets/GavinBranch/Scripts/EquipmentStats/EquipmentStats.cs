using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EquipmentStats : MonoBehaviour
{
    public int Id;
    public string statText;
    public TextMeshProUGUI StatText;
    public TextMeshProUGUI StatName;
    public GameObject StatStorage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StatText.text = " " + StatStorage.GetComponent<EquipmentStatStorage>().baseStats[Id] + "+" + StatStorage.GetComponent<EquipmentStatStorage>().EquipmentStatsArray[Id];
        StatName.text = statText;
    }
}