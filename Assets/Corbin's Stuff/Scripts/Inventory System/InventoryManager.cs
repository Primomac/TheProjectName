using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public bool inventoryIsClosed = true;
    public GameObject inventory;

    public Toggle enableSell;

    InventoryItemController[] inventoryItemsArray;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inventoryIsClosed && !GameObject.Find("ShopManager").GetComponent<ShopController>().shopIsOpen)
        {
            inventoryIsClosed = false;
            inventory.SetActive(true);
            ListItems();
            GameObject.Find("EquipManager").GetComponent<EquipManager>().equipMenu.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.E) && !inventoryIsClosed)
        {
            inventoryIsClosed = true;
            inventory.SetActive(false);
            Clean();
            GameObject.Find("EquipManager").GetComponent<EquipManager>().equipMenu.SetActive(false);
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
        Clean();

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var sellButton = obj.transform.Find("SellButton").GetComponent<Button>();
            var equipButton = obj.transform.Find("EquipButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (item.equipable && !GameObject.Find("ShopManager").GetComponent<ShopController>().shopIsOpen)
            {
                equipButton.gameObject.SetActive(true);
            }

            if (GameObject.Find("ShopManager").GetComponent<ShopController>().shopIsOpen && obj.transform.IsChildOf(GameObject.Find("InventoryContent").transform))
            {
                sellButton.gameObject.SetActive(true);
            }
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

        for (int i = 0; i < Items.Count; i++)
        {
            inventoryItemsArray[i].AddItem(Items[i]);
        }
    }

    public void Clean()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }
}