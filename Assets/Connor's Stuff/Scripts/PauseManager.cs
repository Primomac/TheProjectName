using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseControlDisplay;
    public GameObject controlsMenu;
    public GameObject pauseMenu;
    public bool isPaused;

    public static GameObject pmInstance;
    

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = PauseInstance.pauseInstance;
        if (pmInstance == null)
        {
            pmInstance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        controlsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Title Scene" && SceneManager.GetActiveScene().name != "Desert_CombatScene"
            && SceneManager.GetActiveScene().name != "CombatScene 1" && SceneManager.GetActiveScene().name != "Desert_CombatScene_Boss"
            && SceneManager.GetActiveScene().name != "CombatScene_ForestBoss" && SceneManager.GetActiveScene().name != "VictoryScene")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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
                    controlsMenu.SetActive(false);

                }

                isPaused = !isPaused;
            }

            if(!isPaused && InventoryManager.instance.inventoryIsClosed)
            {
                pauseControlDisplay.SetActive(true);
            }
            else
            {
                pauseControlDisplay.SetActive(false);
            }
            
        }
        else
        {
            pauseControlDisplay.SetActive(false);
        }
    }

    public void Unpause()
    {
        isPaused = false;
    }
}
