using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transitionAnim;
    private PlayerInteractions player;
    private PlayerSceneSwap playerSceneController;

    public Image sceneTransitionImage;
    public string transitionAnimName;
    public float timeToLoadFor = 1f;

    private void Start()
    {
        if(GameObject.Find("Player"))
        {
            player = GameObject.Find("Player").GetComponent<PlayerInteractions>();
            playerSceneController = GameObject.Find("Player").GetComponent<PlayerSceneSwap>();
        }
        transitionAnim = GameObject.Find("SceneTransition").GetComponent<Animator>();
        sceneTransitionImage = GameObject.Find("SceneTransition").GetComponent<Image>();
        StartCoroutine(ResetTransitionImagePosition());
    }

    public IEnumerator LoadLevel(string sceneToSwapTo)
    {
        player.isTransitioning = true;
        transitionAnim = playerSceneController.sceneTransitionAnim;

        transitionAnimName = playerSceneController.sceneAnimToPlay;

        if(playerSceneController.sceneTransitionImage != null)
        {
            sceneTransitionImage.sprite = playerSceneController.sceneTransitionImage;
        }

        transitionAnim.Play(transitionAnimName);
        yield return new WaitForSeconds(timeToLoadFor);


        player.dialogueBox.gameObject.SetActive(true);
        player.isTransitioning = false;
        SceneManager.LoadScene(sceneToSwapTo);
    }

    public IEnumerator ResetTransitionImagePosition()
    {
        yield return new WaitForSeconds(0.52f);
        sceneTransitionImage.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1398);
    }
}
