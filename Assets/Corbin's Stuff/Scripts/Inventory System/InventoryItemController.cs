using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public void AddItem(Item newItem)
    {

        item = newItem;
    }

    public void RemoveItem()
    {
        InventoryManager.instance.RemoveTheItem(item);
    }

    public void SellItem()
    {
        Debug.Log(item.sellValue);
        CoinsController.coinAmount += item.sellValue;
        InventoryManager.instance.RemoveTheItem(item);
        Destroy(gameObject);
    }
}