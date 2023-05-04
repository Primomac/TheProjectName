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

    private void Awake()
    {
        equipInstance = this;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            FindStatStorage();
        }
    }
    public void FindStatStorage()
    {
        statStorage = GameObject.Find("StatMenu");
        Debug.Log("Grabbed storage!");
    }

    public void Add(Item item)
    {
        if(statStorage == null)
        {
            Debug.Log("Storage Found");
            FindStatStorage();
        }
        Items.Add(item);
        EquipmentStatStorage.storageInstance.changeStats();
        ItemManager.instance.equippedItems.Add(item);
    }

    public void Remove(Item item)
    {
        if (statStorage == null)
        {
            FindStatStorage();
            Debug.Log("Storage Found");
        }
        Items.Remove(item);
        EquipmentStatStorage.storageInstance.changeStats();
        ItemManager.instance.equippedItems.Remove(item);
    }
    private void OnApplicationQuit()
    {
        //itemManager.AddItems(items);
    }

}
