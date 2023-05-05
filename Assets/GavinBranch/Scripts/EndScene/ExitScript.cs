using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    

    public void ExitToTitleScreen()
    {
        SceneManager.LoadScene("Title Scene");
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
