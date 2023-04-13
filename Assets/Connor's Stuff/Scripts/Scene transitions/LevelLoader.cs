using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transitionAnim;
    private PlayerInteractions player;

    public float timeToLoadFor = 1f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerInteractions>();
    }

    public IEnumerator LoadLevel(string sceneToSwapTo)
    {
        //transitionAnim.SetTrigger("Start");
        player.isTransitioning = true;

        yield return new WaitForSeconds(timeToLoadFor);

        player.isTransitioning = false;
        SceneManager.LoadScene(sceneToSwapTo);
    }
}
