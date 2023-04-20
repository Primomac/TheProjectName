/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Item> items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    bool inventoryIsClosed = true;
    public GameObject inventory;

    public Toggle enableSell;

    InventoryItemController[] inventoryItemsArray;

    ItemManager itemManager; // reference to ItemManager script

    private void Awake()
    {
        instance = this;
        itemManager = GetComponent<ItemManager>(); // get the reference to the ItemManager script

        Debug.Log(items);
    }

    private void Update()
    {
        SaveItemsToItemManager();
        if (Input.GetKeyDown(KeyCode.E) && inventoryIsClosed)
        {
            inventoryIsClosed = false;
            inventory.SetActive(true);
            ListItems();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryIsClosed = true;
            inventory.SetActive(false);
            Clean();
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

        // Use the items list from the ItemManager script
        foreach (var item in itemManager.items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var sellButton = obj.transform.Find("SellButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (enableSell.isOn)
                sellButton.gameObject.SetActive(true);
        }

        SetInventoryItems();
    }

    public void EnableItemsSell()
    {
        if (enableSell.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("SellButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("SellButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        inventoryItemsArray = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < itemManager.items.Count; i++)
        {
            inventoryItemsArray[i].AddItem(itemManager.items[i]);
        }
    }

    void Clean()
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
*/