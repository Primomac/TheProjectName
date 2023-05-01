using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager equipInstance;
    public List<Item> Items = new List<Item>();
         
    public Transform itemContent;
    public GameObject equipItem;

    public GameObject equipMenu;
    public GameObject statStorage;

    public int itemsEquipped = 0;
    public int weaponsEquipped = 0;

    StatSheet statSheet;

    private void Awake()
    {
        equipInstance = this;
        statSheet = GameObject.Find("Player").GetComponent<StatSheet>();
    }

    public void Add(Item item)
    {
        Items.Add(item);
        statStorage.GetComponent<EquipmentStatStorage>().changeStats();
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
        statStorage.GetComponent<EquipmentStatStorage>().changeStats();
    }
}
