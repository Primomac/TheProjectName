using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDuplicateCanvas : MonoBehaviour
{
    private static GameObject instance;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
    }
}
