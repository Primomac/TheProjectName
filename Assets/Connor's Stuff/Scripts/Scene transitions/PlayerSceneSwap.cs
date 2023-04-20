using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneSwap : MonoBehaviour
{
    public LevelLoader levelLoader;
    public string sceneToSwapTo;

    public Animator sceneTransitionAnim;
    public string sceneAnimToPlay;
    public Sprite sceneTransitionImage;

    private void Start()
    {
        levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();

        sceneTransitionAnim = GameObject.Find("SceneTransition").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Scene Trigger"))
        {
            sceneToSwapTo = collision.gameObject.GetComponent<TriggerSceneSwap>().sceneToSwapTo;
            
            if(collision.gameObject.GetComponent<TriggerSceneSwap>().sceneTransitionSprite != null)
            {
                sceneTransitionImage = collision.gameObject.GetComponent<TriggerSceneSwap>().sceneTransitionSprite;
            }

            sceneAnimToPlay = collision.gameObject.GetComponent<TriggerSceneSwap>().transitionAnimName;

            levelLoader.StartCoroutine(levelLoader.LoadLevel(sceneToSwapTo));
            
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
            levelLoader.StartCoroutine(levelLoader.ResetTransitionImagePosition());
        }
    }
}
