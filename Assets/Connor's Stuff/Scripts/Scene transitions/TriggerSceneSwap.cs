using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneSwap : MonoBehaviour
{
    public string sceneToSwapTo;

    public int levelIndex;

    public AnimationClip transitionAnim;
    public string transitionAnimName;

    public Sprite sceneTransitionSprite;

    private void Start()
    {
        transitionAnimName = transitionAnim.name;
    }
}
