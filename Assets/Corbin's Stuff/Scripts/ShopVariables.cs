using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopVariables : MonoBehaviour
{
    public Transform shopContent;
    public GameObject shopItemMenu;

    public static bool shopkeeperExists;

    ShopController shopController;

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
        if (GameObject.FindGameObjectWithTag("Shopkeeper") == null)
        {
            shopkeeperExists = false;
            Debug.Log("Shopkeeper is fake and untrue, literally clickbait.");
        }
        else
        {
            shopkeeperExists = true;
            Debug.Log("Shopkeeper exists!");
        }

        if (shopkeeperExists)
        {
            shopController = GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<ShopController>();
            shopController.shopItemContent = shopContent;
            shopController.shopMenu = shopItemMenu;
        }
    }
}
