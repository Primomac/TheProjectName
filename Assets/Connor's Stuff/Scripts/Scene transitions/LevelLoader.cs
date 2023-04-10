using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transitionAnim;

    public float timeToLoadFor = 1f;

    public IEnumerator LoadLevel(string sceneToSwapTo)
    {
        //transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(timeToLoadFor);

        SceneManager.LoadScene(sceneToSwapTo);
    }
}
