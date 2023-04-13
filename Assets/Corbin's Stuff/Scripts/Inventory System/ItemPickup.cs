using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    
    bool isInTrigger;
    void Pickup()
    {
        InventoryManager.instance.Add(item);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isInTrigger)
        {
            Pickup();
        }
    }
}
