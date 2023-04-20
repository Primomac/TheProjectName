using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public static ShopController shopInstance;
    public List<Item> Items = new List<Item>();

    public Transform shopItemContent;
    public GameObject shopItem;

    public GameObject shopMenu;

    public bool shopIsOpen;

    InventoryItemController[] shopItemsArray;

    private void Awake()
    {
        shopInstance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !shopIsOpen && GameObject.Find("InventoryManager").GetComponent<InventoryManager>().inventoryIsClosed)
        {
            shopIsOpen = true;
            shopMenu.SetActive(true);
            ListShopItems();
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().inventory.SetActive(true);
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().ListItems();
        }
        else if (Input.GetKeyDown(KeyCode.R) && shopIsOpen)
        {
            shopIsOpen = false;
            shopMenu.SetActive(false);
            CleanShop();
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().inventory.SetActive(false);
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().Clean();
        }
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
}
