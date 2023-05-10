using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public List<Item> items;
    public List<Item> equippedItems;
    public List<int> itemIDs;
    public static ItemManager instance;
    public EquipManager equipManager;

    void Awake()
    {
        instance = this;
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
    public void AddEquipment(List<Item> equipmentToAdd)
    {
        equippedItems.AddRange(equipmentToAdd);
    }

    public void SaveItems()
    {
        string json = JsonUtility.ToJson(new ItemsWrapper { Items = items, EquippedItems = equippedItems });
        File.WriteAllText(Application.persistentDataPath + "/items.json", json);
    }

    public void LoadItems()
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
    }

    void OnApplicationQuit()
    {
        SaveItems();
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
        LoadItems();
    }
}


