using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Storage : MonoBehaviour
{

    public StatSheet currentStats;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "VictoryScene")
        {
            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            manager.stat = currentStats;
        }
        
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            currentStats.UpdateStats(GameObject.FindGameObjectWithTag("Player").GetComponent<StatSheet>());
            Debug.Log("Current EXP (Storage) is " + currentStats.exp);
            Debug.Log("Player's EXP is " + GameObject.FindGameObjectWithTag("Player").GetComponent<StatSheet>().exp + "!");
            Destroy(gameObject);
        }
    }
}
