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

    ItemManager itemManager;

    private void Awake()
    {
        equipInstance = this;
        itemManager = GetComponent<ItemManager>(); // get the reference to the ItemManager script
    }

    private void Update()
    {
        SaveItemsToItemManager();

        if (Input.GetKeyDown(KeyCode.E))
        {
            FindStatStorage();
        }
    }

    private void Start()
    {
        LoadItemsFromItemManager();
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
    }

    void LoadItemsFromItemManager()
    {
        ItemManager itemManager = FindObjectOfType<ItemManager>();
        if (itemManager != null)
        {
            Items.AddRange(itemManager.equipped);
        }
    }

    void SaveItemsToItemManager()
    {
        if (itemManager != null)
        {
            itemManager.equipped = Items;
        }
    }
    private void OnApplicationQuit()
    {
        //itemManager.AddItems(items);
    }
}
