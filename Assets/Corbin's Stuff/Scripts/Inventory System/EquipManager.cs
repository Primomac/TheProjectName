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
    InventoryItemController[] equipItemsArray;
    InventoryItemController[] equipWeaponsArray;
         
    public Transform itemContent;
    public Transform weaponContent;
    public GameObject equipItem;

    public GameObject inventoryMenu;
    public GameObject equipMenu;
    public GameObject statStorage;

    public int itemsEquipped = 0;
    public int weaponsEquipped = 0;

    InventoryItemController iic;
    InventoryItemController[] iicArray;

    public InventoryItemController[] equipArray;

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
        if (SceneManager.GetActiveScene().name != "Title Scene" && SceneManager.GetActiveScene().name != "ManagerScene" && SceneManager.GetActiveScene().name != "CombatScene" && SceneManager.GetActiveScene().name != "Desert_CombatScene")
        {
            inventoryMenu.SetActive(true);
            equipMenu.SetActive(true);
            iicArray = FindObjectsOfType<InventoryItemController>();
            Debug.Log("iic Length: " + iicArray.Length);
            foreach (InventoryItemController iic in iicArray)
            {
                Debug.Log("iic Length part 2 electric boogaloo: " + iicArray.Length);
                Debug.Log("iic found on: " + iic.gameObject.name);
                if (iic != null)
                {
                    iic.SetSkills();
                    Debug.Log("Skills set! ...Finally...");

                    Item item = iic.item;
                    if (item != null && Items.Contains(item))
                    {
                        iic.itemIsEquipped = true;
                    }
                }
                else
                {
                    Debug.Log("WHYYYYYYYYY");
                }
            }
            inventoryMenu.SetActive(false);
            equipMenu.SetActive(false);
        }
    }

    private void Awake()
    {
        equipInstance = this;
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

        foreach(Item item in Items)
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
        for (int i = 0; i < Items.Count; i++)
        {
            equipWeaponsArray[i].AddItem(Items[i]);
        }

        equipItemsArray = itemContent.GetComponentsInChildren<InventoryItemController>();
        if (equipItemsArray.Length > 0)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                equipItemsArray[i].AddItem(Items[i]);
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
