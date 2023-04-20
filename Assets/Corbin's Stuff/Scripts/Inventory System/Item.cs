using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
    public int sellValue;
    public int shopValue;
    public ItemType itemType;
    public bool equipable;

    [Serializable]
    public enum ItemType
    {
        Weapon,
        Jinx,
        Collectible,
        Armor
    }
    private void Awake()
    {
        float x = sellValue * .20f;
        shopValue += Mathf.RoundToInt(x) + sellValue;
    }
}
