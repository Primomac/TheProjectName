using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string startingSceneName;
    public void StartGame()
    {
        SceneManager.LoadScene(startingSceneName);
    }
}
