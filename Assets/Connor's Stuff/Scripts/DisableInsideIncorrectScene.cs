using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableInsideIncorrectScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Title Scene" && SceneManager.GetActiveScene().name != "Desert_CombatScene"
            && SceneManager.GetActiveScene().name != "CombatScene 1" && SceneManager.GetActiveScene().name != "Desert_CombatScene_Boss"
            && SceneManager.GetActiveScene().name != "CombatScene_ForestBoss" && SceneManager.GetActiveScene().name != "VictoryScene")
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
