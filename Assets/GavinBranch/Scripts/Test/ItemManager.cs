using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ItemManager : MonoBehaviour
{
    public List<Item> items;
    public List<Item> equipped;
    public ItemManager instance;

    void awake()
    {
        instance = this;
    }

    [System.Serializable]
    private class ItemsWrapper
    {
        public List<Item> Items;
        public List<Item> Equipped;
    }
    public void AddItems (List<Item> itemsToAdd)
    {
        items.AddRange(itemsToAdd);
    }
    public void AddEquipment(List<Item> equipmentToAdd)
    {
        equipped.AddRange(equipmentToAdd);
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

    void saveEquipment()
    {
        string json = JsonUtility.ToJson(new ItemsWrapper { Equipped =  equipped});
        File.WriteAllText(Application.persistentDataPath + "/skills.json", json);
    }

    void LoadEquipment()
    {
        if (File.Exists(Application.persistentDataPath + "/skills.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/skills.json");
            equipped = JsonUtility.FromJson<ItemsWrapper>(json).Equipped;
        }
    }

    void OnApplicationQuit()
    {
        SaveItems();
        saveEquipment();
    }

    void Awake()
    {
        LoadItems();
        LoadEquipment();
    }
}


