using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;
    public bool isEquipped;
    public bool isInShop;

    private void Start()
    {
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

        if (GameObject.Find("ShopManager").GetComponent<ShopController>().shopIsOpen)
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

        if(!isInShop && GameObject.Find("ShopManager").GetComponent<ShopController>().shopIsOpen)
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
    }

    public void RemoveItem()
    {
        InventoryManager.instance.Remove(item);
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
        if (item.equipable == true && !isEquipped && !GameObject.Find("ShopManager").GetComponent<ShopController>().shopIsOpen)
        {
            InventoryManager.instance.Remove(item);
            EquipManager.equipInstance.Add(item);
            transform.SetParent(GameObject.Find("EquipContent").transform);
            isEquipped = true;
            Debug.Log("Item Equipped");
        }
        else if (item.equipable == true && !GameObject.Find("ShopManager").GetComponent<ShopController>().shopIsOpen)
        {
            EquipManager.equipInstance.Remove(item);
            InventoryManager.instance.Add(item);
            transform.SetParent(GameObject.Find("InventoryContent").transform);
            isEquipped = false;
            Debug.Log("Item Unequiped");
        }
    }
}