using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneSwap : MonoBehaviour
{
    public string sceneToSwapTo;

    public int levelIndex;

    private void Start()
    {
        levelIndex = SceneManager.GetSceneByName(sceneToSwapTo).buildIndex;
    }
}
