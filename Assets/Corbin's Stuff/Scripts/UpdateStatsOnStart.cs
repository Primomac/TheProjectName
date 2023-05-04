using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateStatsOnStart : MonoBehaviour
{
    public EquipmentStatStorage est;

    public GameManager gm;

    // Start is called before the first frame update
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().name != "ManagerScene" && SceneManager.GetActiveScene().name != "Title Scene" &&
            SceneManager.GetActiveScene().name != "CombatScene" && SceneManager.GetActiveScene().name != "Desert_CombatScene"
            && SceneManager.GetActiveScene().name != "VictoryScene")
        {
            if (est.player == null)
            {
                est.player = GameObject.Find("Player").GetComponent<StatSheet>();
                est.changeStats();
            }
        }
    }
}
