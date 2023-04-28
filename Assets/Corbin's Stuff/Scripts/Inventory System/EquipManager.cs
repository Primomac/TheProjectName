using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager equipInstance;
    public List<Item> Items = new List<Item>();
    public List<Skill> skills = new List<Skill>();
         
    public Transform itemContent;
    public GameObject equipItem;

    public GameObject equipMenu;
    public GameObject statStorage;

    public int itemsEquipped = 0;
    public int weaponsEquipped = 0;

    private void Awake()
    {
        equipInstance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
        statStorage.GetComponent<EquipmentStatStorage>().changeStats();
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
        statStorage.GetComponent<EquipmentStatStorage>().changeStats();
    }

    public void SetSkills()
    {
        List<Skill> skillList = GameObject.FindGameObjectWithTag("Player").GetComponent<StatSheet>().skillList;
        foreach (Skill skill in skillList)
        {
            skills.Add(skill);
        }
    }

}
