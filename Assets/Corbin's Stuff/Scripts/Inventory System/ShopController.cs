using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public static ShopController shopInstance;
    public List<Item> Items = new List<Item>();
    public List<Item> itemsKept = new List<Item>();

    public Transform shopItemContent;
    public GameObject shopItem;

    public GameObject shopMenu;

    public bool shopIsOpen;

    public InventoryItemController[] shopItemsArray;

    InventoryManager inventoryManager;

    public ShopVariables shopVariables;

    private void Awake()
    {
        shopInstance = this;
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        shopVariables = GameObject.Find("StoreShopStuff").GetComponent<ShopVariables>();
    }

    private void Start()
    {

    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }


    public void ListShopItems()
    {
        CleanShop();
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(shopItem, shopItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var buyButton = obj.transform.Find("BuyButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (shopIsOpen && obj.transform.IsChildOf(GameObject.Find("ShopContent").transform))
            {
                buyButton.gameObject.SetActive(true);
            }
        }
        SetShopItems();
    }

    public void SetShopItems()
    {
        shopItemsArray = shopItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            shopItemsArray[i].AddItem(Items[i]);
        }
    }

    void CleanShop()
    {
        foreach (Transform item in shopItemContent)
        {
            Destroy(item.gameObject);
        }
    }

    void DeleteSoldItems()
    {
        InventoryItemController[] soldItems = shopItemContent.GetComponentsInChildren<InventoryItemController>();
        foreach (InventoryItemController item in soldItems)
        {
            if(item.tag == "SoldItem")
            {
                item.RemoveShopItem();
            }    
        }
    }

    public void OpenShop()
    {
        if (!shopIsOpen && GameObject.Find("InventoryManager").GetComponent<InventoryManager>().inventoryIsClosed)
        {
            shopIsOpen = true;
            inventoryManager.inventoryIsClosed = false;
            Debug.Log(shopMenu);
            shopMenu.SetActive(true);
            ListShopItems();
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().inventory.SetActive(true);
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().ListItems();
        }
    }

    public void CloseShop()
    {
        if(shopIsOpen)
        {
            shopIsOpen = false;
            inventoryManager.inventoryIsClosed = true;
            shopMenu.SetActive(false);
            CleanShop();
            DeleteSoldItems();
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().inventory.SetActive(false);
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().Clean();
        }
    }
}
