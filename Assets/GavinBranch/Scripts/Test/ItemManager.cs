using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> items;
    public List<Item> equippedItems;
    public static ItemManager instance;

    void Awake()
    {
        instance = this;
        LoadItems();
    }

    [System.Serializable]
    private class ItemsWrapper
    {
        public List<Item> Items;
        public List<Item> EquippedItems;
    }

    public void AddItems(List<Item> itemsToAdd)
    {
        items.AddRange(itemsToAdd);
    }

    public void AddEquippedItems(List<Item> equippedItemsToAdd)
    {
        equippedItems.AddRange(equippedItemsToAdd);
    }

    void SaveItems()
    {
        string json = JsonUtility.ToJson(new ItemsWrapper { Items = items, EquippedItems = equippedItems });
        File.WriteAllText(Application.persistentDataPath + "/items.json", json);
    }

    void LoadItems()
    {
        if (File.Exists(Application.persistentDataPath + "/items.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/items.json");
            ItemsWrapper wrapper = JsonUtility.FromJson<ItemsWrapper>(json);
            items = wrapper.Items;
            equippedItems = wrapper.EquippedItems;
        }
    }

    void OnApplicationQuit()
    {
        SaveItems();
    }

     void start()
    {
        LoadItems();
    }
}
