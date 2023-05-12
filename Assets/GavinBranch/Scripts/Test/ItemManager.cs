using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public List<Item> items;
    public List<Item> equippedItems;
    public static ItemManager instance;
    public EquipManager equipManager;

    public List<int> itemIDsEquipped = new List<int>();
    public List<int> itemIDs = new List<int>();
    public Item itemToAdd;
    public Item[] myArrayOfScriptableItems;

    void Awake()
    {
        instance = this;
    }

    [System.Serializable]
    private class ItemsWrapper
    {
        public List<Item> Items;
        public List<Item> EquippedItems;
        public List<int> ItemIDs;
    }

    public void AddItems(List<Item> itemsToAdd)
    {
        items.AddRange(itemsToAdd);
    }
    public void AddEquipment(List<Item> equipmentToAdd)
    {
        equippedItems.AddRange(equipmentToAdd);
    }

    /*public void SaveItems()
    {
        string json = JsonUtility.ToJson(new ItemsWrapper { Items = items, EquippedItems = equippedItems });
        File.WriteAllText(Application.persistentDataPath + "/items.json", json);
    }

    //public void LoadItems()
    {
        if (File.Exists(Application.persistentDataPath + "/items.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/items.json");
            ItemsWrapper wrapper = JsonUtility.FromJson<ItemsWrapper>(json);
            items = wrapper.Items;
            equippedItems = wrapper.EquippedItems;

            if (SceneManager.GetActiveScene().name != "Title Scene" && SceneManager.GetActiveScene().name != "ManagerScene")
            {
                foreach (Item item in equippedItems)
                {
                    EquipManager.equipInstance.Items.Add(item);
                }
            }
        }
    }*/

    void OnApplicationQuit()
    {
        getId();
        SaveItemIDs();
        SaveItemIDsEquipped();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().name != "ManagerScene")
        {
            getId();
            SaveItemIDs();
            SaveItemIDsEquipped();
        }
        if (SceneManager.GetActiveScene().name == "ManagerScene")
        {
            LoadItemIDs();
            LoadItemIDsEquipped();
            addItems();
        }
    }
    public void getId()
    {
        itemIDs.Clear();
        itemIDsEquipped.Clear();
        foreach (Item item in items)
        {
            int itemID = item.id;
            itemIDs.Add(itemID);
        }

        foreach (Item item in equippedItems)
        {
            int itemID = item.id;
            itemIDsEquipped.Add(itemID);
        }
    }

    public void SaveItemIDs()
    {
        Debug.Log("ID saved");
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/itemIDs.bin", FileMode.Create);
        BinaryWriter writer = new BinaryWriter(fileStream);

        foreach (int id in itemIDs)
        {
            writer.Write(id);
        }

        writer.Close();
        fileStream.Close();
    }

    public void LoadItemIDs()
    {
        if (File.Exists(Application.persistentDataPath + "/itemIDs.bin"))
        {
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/itemIDs.bin", FileMode.Open);
            BinaryReader reader = new BinaryReader(fileStream);

            List<int> loadedItemIDs = new List<int>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int id = reader.ReadInt32();
                loadedItemIDs.Add(id);
            }

            reader.Close();
            fileStream.Close();

            itemIDs = loadedItemIDs;
            Debug.Log(itemIDs);
        }
    }

    public void SaveItemIDsEquipped()
    {
        Debug.Log("Equipped IDs saved");
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/itemIDsEquipped.bin", FileMode.Create);
        BinaryWriter writer = new BinaryWriter(fileStream);

        foreach (int id in itemIDsEquipped)
        {
            writer.Write(id);
        }

        writer.Close();
        fileStream.Close();
    }

    public void LoadItemIDsEquipped()
    {
        if (File.Exists(Application.persistentDataPath + "/itemIDsEquipped.bin"))
        {
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/itemIDsEquipped.bin", FileMode.Open);
            BinaryReader reader = new BinaryReader(fileStream);

            List<int> loadedItemIDsEquipped = new List<int>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int id = reader.ReadInt32();
                loadedItemIDsEquipped.Add(id);
            }

            reader.Close();
            fileStream.Close();

            itemIDsEquipped = loadedItemIDsEquipped;
            Debug.Log("Equipped IDs loaded");
        }
    }


    public void addItems()
    {
        items.Clear();
        equippedItems.Clear();
        foreach (int itemID in itemIDs)
        {
            // Find the ScriptableObject item with the matching ID
            itemToAdd = myArrayOfScriptableItems[itemID];

            if (itemToAdd != null)
            {
                // Add the item to the itemList
                items.Add(itemToAdd);
            }
            else
            {
                Debug.LogWarning("No item found with ID " + itemID);
            }
        }

        foreach (int itemID in itemIDsEquipped)
        {
            // Find the ScriptableObject item with the matching ID
            itemToAdd = myArrayOfScriptableItems[itemID];

            if (itemToAdd != null)
            {
                // Add the item to the itemList
                items.Add(itemToAdd);
            }
            else
            {
                Debug.LogWarning("No item found with ID " + itemID);
            }
        }
    }
}


