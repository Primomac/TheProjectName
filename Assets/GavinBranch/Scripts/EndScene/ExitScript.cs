using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    public void ExitToTitleScreen()
    {
        SceneManager.LoadScene("Title Scene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
