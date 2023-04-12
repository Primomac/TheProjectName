using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public bool inventoryIsClosed = true;
    public GameObject inventory;

    public Toggle enableSell;

    public InventoryItemController[] inventoryItems;
   //public List<InventoryItemController> inventoryItems = new List<InventoryItemController>();
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inventoryIsClosed)
        {
            inventoryIsClosed = false;
            inventory.SetActive(true);
            ListItems();
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            inventoryIsClosed = true;
            inventory.SetActive(false);
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
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
        if(enableSell.isOn)
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
        inventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            inventoryItems[i].AddItem(Items[i]);
        }


        for (int i = 0; i < Items.Count; i++)
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i].RemoveItem();
            }
        }
    }

}

