using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquipManager : MonoBehaviour
{
    public static EquipManager equipInstance;
    public List<Item> Items = new List<Item>();
    public InventoryItemController[] equipItemsArray;
    public InventoryItemController[] equipWeaponsArray;
         
    public Transform itemContent;
    public Transform weaponContent;
    public GameObject equipItem;

    public GameObject inventoryMenu;
    public GameObject equipMenu;
    public GameObject statStorage;

    public int itemsEquipped = 0;
    public int weaponsEquipped = 0;
    
    public InventoryItemController iic;
    public InventoryItemController[] iicArray;

    ItemManager itemManager;

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
        if (SceneManager.GetActiveScene().name != "Title Scene" && SceneManager.GetActiveScene().name != "ManagerScene" && SceneManager.GetActiveScene().name != "CombatScene 1" && SceneManager.GetActiveScene().name != "Desert_CombatScene")
        {
            InventoryManager.instance.inventoryIsClosed = false;
            inventoryMenu.SetActive(true);
            InventoryManager.instance.ListItems();
            ListEquipItems();
            GameObject.Find("EquipManager").GetComponent<EquipManager>().equipMenu.SetActive(true);
            iicArray = FindObjectsOfType<InventoryItemController>();
            foreach (InventoryItemController iic in iicArray)
            {
                if (iic.transform.parent == weaponContent)
                {
                    Debug.Log("ITS NOT NULL, DIE");
                    if (iic != null)
                    {
                        iic.SetSkills();
                        Debug.Log("Skills set! ...Finally...");
                    }
                    else
                    {
                        Debug.Log("WHYYYYYYYYY");
                    }
                }
            }
            InventoryManager.instance.inventoryIsClosed = true;
            inventoryMenu.SetActive(false);
            InventoryManager.instance.Clean();
            Clean();
            GameObject.Find("EquipManager").GetComponent<EquipManager>().equipMenu.SetActive(false);
        }
    }

    private void Awake()
    {
        equipInstance = this;
    }

    private void Start()
    {
        itemManager = GameObject.Find("InventoryManager").GetComponent<ItemManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            FindStatStorage();
        }
        if (SceneManager.GetActiveScene().name == "CombatScene" || SceneManager.GetActiveScene().name == "Desert_CombatScene" ||
            SceneManager.GetActiveScene().name == "Desert_CombatScene_Boss" || SceneManager.GetActiveScene().name == "CombatScene_ForestBoss")
        {
            inventoryMenu.SetActive(false);
            equipMenu.SetActive(false);
        }
    }
    public void FindStatStorage()
    {
        statStorage = GameObject.Find("StatMenu");
        Debug.Log("Grabbed storage!");
    }

    public void Add(Item item)
    {
        if(statStorage == null)
        {
            Debug.Log("Storage Found");
            FindStatStorage();
        }
        Items.Add(item);
        EquipmentStatStorage.storageInstance.changeStats();
        ItemManager.instance.equippedItems.Add(item);
        iic = FindObjectOfType<InventoryItemController>();
    }

    public void Remove(Item item)
    {
        if (statStorage == null)
        {
            FindStatStorage();
            Debug.Log("Storage Found");
        }
        Items.Remove(item);
        EquipmentStatStorage.storageInstance.changeStats();
        ItemManager.instance.equippedItems.Remove(item);
    }

    public void ListEquipItems()
    {
        Clean();

        foreach(Item item in ItemManager.instance.equippedItems)
        {
            if(item.itemType == Item.ItemType.Weapon)
            {
                GameObject obj = Instantiate(equipItem, weaponContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var equipButton = obj.transform.Find("EquipButton").GetComponent<Button>();

                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;
                equipButton.gameObject.SetActive(true);
            }

            if(item.itemType != Item.ItemType.Weapon)
            {
                GameObject obj = Instantiate(equipItem, itemContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var equipButton = obj.transform.Find("EquipButton").GetComponent<Button>();


                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;
                equipButton.gameObject.SetActive(true);
            }
        }

        SetEquippedItems();
    }

    public void SetEquippedItems()
    {
        equipWeaponsArray = weaponContent.GetComponentsInChildren<InventoryItemController>();

        if (equipWeaponsArray.Length > 0)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (iic != null && iic.item.itemType != Item.ItemType.Weapon)
                {
                    equipWeaponsArray[i].AddItem(Items[i]);
                    weaponsEquipped = equipWeaponsArray.Length;
                }

            }
        }

        equipItemsArray = itemContent.GetComponentsInChildren<InventoryItemController>();
        if (equipItemsArray.Length > 0)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if(iic != null && iic.item.itemType != Item.ItemType.Weapon)
                {
                    equipItemsArray[i].AddItem(Items[i]);
                    itemsEquipped = equipItemsArray.Length;
                }
            }
        }
    }

    public void Clean()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach(Transform item in weaponContent)
        {
            Destroy(item.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        //itemManager.AddItems(items);
    }
}
