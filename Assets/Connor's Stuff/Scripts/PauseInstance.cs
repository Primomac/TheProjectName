using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInstance : MonoBehaviour
{
    public static GameObject pauseInstance;

    // Start is called before the first frame update
    void Start()
    {
        pauseInstance = this.gameObject;
    }
}
