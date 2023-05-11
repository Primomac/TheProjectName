using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    [Header("STUFF THAT YOU SHOULD PROBABLY SET")]
    public int id;
    public string itemName;
    public Sprite icon;
    public int sellValue;
    public ItemType itemType;
    public bool equipable;

    public List<Skill> skills = new List<Skill>();

    [Header("Stuff that's set automatically.")]
    public int shopValue;

    [Header("Stat Modifications")]
    public float strengthModification;
    public float dexterityModification;
    public float soulModification;
    public float gutsModification;
    public float focusModification;
    public float agilityModification;

    [Serializable]
    public enum ItemType
    {
        Weapon,
        Jinx,
        Collectible,
        Armor
    }
    void Awake()
    {
        float x = sellValue * .20f;
        shopValue += Mathf.RoundToInt(x) + sellValue;
    }
}
