using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Item> items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public bool inventoryIsClosed = true;
    public GameObject inventory;

    public Toggle enableSell;

    InventoryItemController[] inventoryItemsArray;

    ItemManager itemManager;
    EquipManager equipManager;
    private void Awake()
    {
        instance = this;
        itemManager = GetComponent<ItemManager>(); // get the reference to the ItemManager script
        equipManager = GameObject.Find("EquipManager").GetComponent<EquipManager>();
    }

    private void Update()
    {
        SaveItemsToItemManager();

        if (ShopVariables.shopkeeperExists || !ShopVariables.shopkeeperExists)
        {
            if (Input.GetKeyDown(KeyCode.E) && inventoryIsClosed && SceneManager.GetActiveScene().name != "Title Scene")
            {
                inventoryIsClosed = false;
                inventory.SetActive(true);
                ListItems();
                equipManager.ListEquipItems();
                GameObject.Find("EquipManager").GetComponent<EquipManager>().equipMenu.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.E) && !inventoryIsClosed)
            {
                inventoryIsClosed = true;
                inventory.SetActive(false);
                Clean();
                equipManager.Clean();
                GameObject.Find("EquipManager").GetComponent<EquipManager>().equipMenu.SetActive(false);
            }
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        LoadItemsFromItemManager();
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        Clean();

        foreach (var item in itemManager.items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var sellButton = obj.transform.Find("SellButton").GetComponent<Button>();
            var equipButton = obj.transform.Find("EquipButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if(ShopVariables.shopkeeperExists)
            {
                if (item.equipable && !GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<ShopController>().shopIsOpen)
                {
                    equipButton.gameObject.SetActive(true);
                }

                if (GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<ShopController>().shopIsOpen && obj.transform.IsChildOf(GameObject.Find("InventoryContent").transform))
                {
                    sellButton.gameObject.SetActive(true);
                }
            }

            if(!ShopVariables.shopkeeperExists && item.equipable)
            {
                equipButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems();
    }

    public void SetInventoryItems()
    {
        inventoryItemsArray = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < items.Count; i++)
        {
            inventoryItemsArray[i].AddItem(items[i]);
        }
    }

    public void Clean()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }

    void LoadItemsFromItemManager()
    {
        ItemManager itemManager = FindObjectOfType<ItemManager>();
        if (itemManager != null)
        {
            items.AddRange(itemManager.items);
        }
    }

    void SaveItemsToItemManager()
    {
        if (itemManager != null)
        {
            itemManager.items = items;
        }
    }
    private void OnApplicationQuit()
    {
        //itemManager.AddItems(items);
    }

}