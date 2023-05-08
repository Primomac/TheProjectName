using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;


    

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = PauseInstance.pauseInstance;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                
            }  

            if (isPaused)
            {
                Time.timeScale = 1.0f;
                pauseMenu.SetActive(false);
                
            }

            isPaused = !isPaused;
        }
    }
}
