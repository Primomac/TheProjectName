using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDuplicateManagers : MonoBehaviour
{
    private static GameObject managersInstance;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (managersInstance == null)
            managersInstance = gameObject;
        else
            Destroy(gameObject);
    }
}
