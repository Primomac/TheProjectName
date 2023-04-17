using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager equipInstance;
    public List<Item> Items = new List<Item>();

    public Transform itemContent;
    public GameObject equipItem;

    public GameObject equipMenu;

    InventoryItemController[] equipItems;

    private void Awake()
    {
        equipInstance = this;
    }
    void Start()
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
}
