using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDuplicateInventory : MonoBehaviour
{
    private static GameObject inventoryInstance;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (inventoryInstance == null)
            inventoryInstance = gameObject;
        else
            Destroy(gameObject);
    }
}
