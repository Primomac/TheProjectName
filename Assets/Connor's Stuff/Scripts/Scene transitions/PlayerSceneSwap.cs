using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneSwap : MonoBehaviour
{
    public LevelLoader levelLoader;

    public int levelIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Scene Trigger"))
        {
            levelIndex = collision.gameObject.GetComponent<TriggerSceneSwap>().levelIndex;
            levelLoader.StartCoroutine(levelLoader.LoadLevel(levelIndex));
        }
    }
}
