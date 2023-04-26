using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ItemManager : MonoBehaviour
{
    public List<Item> items;
    public List<Skill> skills;
    public ItemManager instance;

    void awake()
    {
        instance = this;
    }

    [System.Serializable]
    private class ItemsWrapper
    {
        public List<Item> Items;
        public List<Skill> Skills;
    }
    public void AddItems(List<Item> itemsToAdd)
    {
        items.AddRange(itemsToAdd);
    }
    public void AddSkills(List<Skill> skillsToAdd)
    {
        skills.AddRange(skillsToAdd);
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

    void saveSkills()
    {
        string json = JsonUtility.ToJson(new ItemsWrapper { Skills =  skills});
        File.WriteAllText(Application.persistentDataPath + "/skills.json", json);
    }

    void LoadSkills()
    {
        if (File.Exists(Application.persistentDataPath + "/skills.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/skills.json");
            skills = JsonUtility.FromJson<ItemsWrapper>(json).Skills;
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


