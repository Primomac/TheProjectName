using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("InventoryManager"));
        DontDestroyOnLoad(GameObject.Find("CoinManager"));
        DontDestroyOnLoad(GameObject.Find("Canvas"));
        DontDestroyOnLoad(GameObject.Find("EventSystem"));
        DontDestroyOnLoad(GameObject.Find("Player"));
        DontDestroyOnLoad(gameObject);
    }
}
