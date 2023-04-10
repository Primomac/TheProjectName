using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneSwap : MonoBehaviour
{
    public LevelLoader levelLoader;
    public string sceneToSwapTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Scene Trigger"))
        {
            levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();

            sceneToSwapTo = collision.gameObject.GetComponent<TriggerSceneSwap>().sceneToSwapTo;
            levelLoader.StartCoroutine(levelLoader.LoadLevel(sceneToSwapTo));
        }
    }
}
