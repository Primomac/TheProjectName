using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterStart : MonoBehaviour
{
    // Variables

    public string battleScene;
    public string encounterScene;
    public Vector2 encounterPosition;
    public List<StatSheet> enemyStats = new List<StatSheet>();
    public AudioClip encounterSound;
    public AudioClip battleMusic;
    
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Play encounterSound
            // Cool battle transition
            encounterPosition = collision.transform.position;
            collision.gameObject.GetComponent<PlayerController>().encounterPosition = encounterPosition;
            enemyStats.Add(collision.GetComponent<StatSheet>());
            collision.gameObject.SetActive(false);
            if (GameObject.FindGameObjectWithTag("Enemy"))
            {
                List<GameObject> encounters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
                encounters.Remove(gameObject);
                for (int i = 0; i < encounters.Count; i++)
                {
                    Debug.Log("Destroying " + encounters[i].name);
                    Destroy(encounters[i]);
                }
            }
            Debug.Log("Starting newScene()!");
            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            manager.stat = collision.GetComponent<StatSheet>();
            manager.AutoSavePlayer();
            newScene();
        }
    }

    public void newScene()
    {
        SceneManager.LoadScene("" + battleScene);
        Debug.Log(SceneManager.GetActiveScene().name + " loaded!");
        if (SceneManager.GetActiveScene().name != battleScene)
        {
            Debug.Log("Starting waitForSceneLoad()!");
            StartCoroutine("waitForSceneLoad");
        }
        else
        {
            Debug.Log("The current Instance is " + BattleManager.Instance + "!");
            Debug.Log(SceneManager.GetActiveScene().name + " loaded!");
            foreach (StatSheet enemy in enemyStats)
            {
                BattleManager.Instance.AddCombatant(enemy);
            }
            //BattleManager.Instance.AddCombatant(collision.gameObject.GetComponent<StatSheet>());
            BattleManager.Instance.encounterScene = encounterScene;
            //BattleManager.Instance.tickInitiative = true;
            //BattleManager.Instance.inBattle = true;
        }

    }

    IEnumerable waitForSceneLoad()
    {
        Debug.Log("Waiting until the correct scene is loaded!");
        while (SceneManager.GetActiveScene().name != battleScene)
        {
            yield return new WaitForSeconds(0.1f);
            //yield return null;
        }

        if (SceneManager.GetActiveScene().name == battleScene)
        {
            Debug.Log("The current Instance is " + BattleManager.Instance + "!");
            Debug.Log(SceneManager.GetActiveScene().name + " loaded!");
            foreach (StatSheet enemy in enemyStats)
            {
                BattleManager.Instance.AddCombatant(enemy);
            }
            //BattleManager.Instance.AddCombatant(collision.gameObject.GetComponent<StatSheet>());
            BattleManager.Instance.encounterScene = encounterScene;
            BattleManager.Instance.battleMusic = battleMusic;
            BattleManager.Instance.tickInitiative = true;
            BattleManager.Instance.inBattle = true;
            StopCoroutine("waitForSceneLoad");
        }
    }
}
