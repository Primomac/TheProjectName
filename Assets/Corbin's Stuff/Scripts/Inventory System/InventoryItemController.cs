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
}



//Old, decided on a selling mechanic rather than dropping items, but may change back to item dropping in the future.

    /*public void DropItem()
    {
        GameObject player;
        player = GameObject.Find("Player");

        Instantiate(gameObject, player.transform.position, transform.rotation);
        Destroy(gameObject);
        InventoryManager.instance.Remove(item);
    }
    */