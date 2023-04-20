using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ItemManager : MonoBehaviour
{
    public List<Item> items;
    public ItemManager instance;

    void awake()
    {
        instance = this;
    }

    [System.Serializable]
    private class ItemsWrapper
    {
        public List<Item> Items;
    }
    public void AddItems(List<Item> itemsToAdd)
    {
        items.AddRange(itemsToAdd);
    }

    void SaveItems()
    {
        string json = JsonUtility.ToJson(new ItemsWrapper { Items = items });
        File.WriteAllText(Application.persistentDataPath + "/items.json", json);
    }

    void LoadItems()
    {
        if (File.Exists(Application.persistentDataPath + "/items.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/items.json");
            items = JsonUtility.FromJson<ItemsWrapper>(json).Items;
        }
    }

    void OnApplicationQuit()
    {
        SaveItems();
    }

    void Awake()
    {
        LoadItems();
    }
}


