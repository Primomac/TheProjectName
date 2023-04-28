using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;
    public bool weaponIsEquipped;
    public bool itemIsEquipped;
    public bool isInShop;
    public bool itemSold;

    EquipManager equipManager;

    private void Start()
    {
        equipManager = GameObject.Find("EquipManager").GetComponent<EquipManager>();

        var sellText = transform.Find("SellPriceText").GetComponent<TextMeshProUGUI>();
        var buyText = transform.Find("BuyPriceText").GetComponent<TextMeshProUGUI>();
        sellText.text = item.sellValue.ToString();
        buyText.text = item.shopValue.ToString();
    }

    private void Update()
    {
        var sellButton = transform.Find("SellButton").GetComponent<Button>();
        var buyButton = transform.Find("BuyButton").GetComponent<Button>();
        var sellText = transform.Find("SellPriceText").GetComponent<TextMeshProUGUI>();
        var buyText = transform.Find("BuyPriceText").GetComponent<TextMeshProUGUI>();

        if (GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<ShopController>().shopIsOpen)
        {
            if (gameObject.transform.IsChildOf(GameObject.Find("ShopContent").transform))
            {
                isInShop = true;
            }
        }
        else
        {
            isInShop = false;
        }

        if(isInShop)
        {
            sellButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            buyText.gameObject.SetActive(true);
            sellText.gameObject.SetActive(false);
        }

        if(!isInShop && GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<ShopController>().shopIsOpen)
        {
            sellButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
            buyText.gameObject.SetActive(false);
            sellText.gameObject.SetActive(true);
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        var sellText = transform.Find("SellPriceText").GetComponent<TextMeshProUGUI>();
        var buyText = transform.Find("BuyPriceText").GetComponent<TextMeshProUGUI>();
        sellText.text = item.sellValue.ToString();
        buyText.text = item.shopValue.ToString();
    }

    public void RemoveItem()
    {
        InventoryManager.instance.Remove(item);
    }

    public void RemoveShopItem()
    {
        ShopController.shopInstance.Remove(item);
    }

    public void SellItem()
    {
        var sellButton = transform.Find("SellButton").GetComponent<Button>();
        var buyButton = transform.Find("BuyButton").GetComponent<Button>();
        CoinsController.coinAmount += item.sellValue;
        InventoryManager.instance.Remove(item);
        ShopController.shopInstance.Add(item);
        if (transform.IsChildOf(GameObject.Find("ShopContent").transform))
        {
            buyButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(false);
        }
        transform.SetParent(GameObject.Find("ShopContent").transform);
        isInShop = true;
        tag = "SoldItem";
    }

    public void BuyItem()
    {
        var sellButton = transform.Find("SellButton").GetComponent<Button>();
        var buyButton = transform.Find("BuyButton").GetComponent<Button>();
        if (CoinsController.coinAmount >= item.shopValue)
        {
            CoinsController.coinAmount -= item.shopValue;
            ShopController.shopInstance.Remove(item);
            InventoryManager.instance.Add(item);
            transform.SetParent(GameObject.Find("InventoryContent").transform);
            if (transform.IsChildOf(GameObject.Find("InventoryContent").transform))
            {
                buyButton.gameObject.SetActive(false);
                sellButton.gameObject.SetActive(true);
            }
            isInShop = false;
        }
    }

    public void EquipItem()
    {
        if (item.itemType == Item.ItemType.Weapon && !weaponIsEquipped && item.equipable && equipManager.weaponsEquipped < 2)
        {
            weaponIsEquipped = true;
            InventoryManager.instance.Remove(item);
            EquipManager.equipInstance.Add(item);
            transform.SetParent(GameObject.Find("WeaponContent").transform);
            Debug.Log("Item Equipped");
        }

        else if (item.equipable && weaponIsEquipped)
        {
            EquipManager.equipInstance.Remove(item);
            InventoryManager.instance.Add(item);
            transform.SetParent(GameObject.Find("InventoryContent").transform);
            weaponIsEquipped = false;
            Debug.Log("Item Unequiped");
            equipManager.weaponsEquipped--;
        }

        if (weaponIsEquipped)
        {
            equipManager.weaponsEquipped++;
            Debug.Log("Items Equipped: " + equipManager.weaponsEquipped);
        }

        if ((item.itemType == Item.ItemType.Collectible || item.itemType == Item.ItemType.Armor) && !itemIsEquipped && item.equipable && equipManager.itemsEquipped < 3)
        {
            itemIsEquipped = true;
            InventoryManager.instance.Remove(item);
            EquipManager.equipInstance.Add(item);
            transform.SetParent(GameObject.Find("EquipContent").transform);
            Debug.Log("Item Equipped");
        }

        else if (item.equipable && itemIsEquipped)
        {
            EquipManager.equipInstance.Remove(item);
            InventoryManager.instance.Add(item);
            transform.SetParent(GameObject.Find("InventoryContent").transform);
            itemIsEquipped = false;
            Debug.Log("Item Unequiped");
            equipManager.itemsEquipped--;
        }

        if (itemIsEquipped)
        {
            equipManager.itemsEquipped++;
            Debug.Log("Items Equipped: " + equipManager.itemsEquipped);
        }
    }
}