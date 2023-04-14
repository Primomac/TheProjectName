using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public bool isEquipped;

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
        CoinsController.coinAmount += item.sellValue;
        InventoryManager.instance.Remove(item);
        Destroy(gameObject);
    }

    public void EquipItem()
    {
        if (item.equipable == true && !isEquipped)
        {
            InventoryManager.instance.Remove(item);
            EquipManager.equipInstance.Add(item);
            transform.SetParent(GameObject.Find("EquipContent").transform);
            isEquipped = true;
            Debug.Log("Item Equipped");
        }
        else if (item.equipable == true)
        {
            EquipManager.equipInstance.Remove(item);
            InventoryManager.instance.Add(item);
            transform.SetParent(GameObject.Find("InventoryContent").transform);
            isEquipped = false;
            Debug.Log("Item Unequiped");
        }
    }
}