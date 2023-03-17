using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterStart : MonoBehaviour
{
    // Variables

    public Scene battleScene;
    public Scene encounterScene;
    public Vector2 encounterPosition;
    public List<StatSheet> enemyStats = new List<StatSheet>();
    public AudioClip encounterSound;
    
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        encounterScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Play encounterSound
            // Cool battle transition
            encounterPosition = collision.transform.position;
            collision.gameObject.GetComponent<PlayerController>().encounterPosition = encounterPosition;
            SceneManager.LoadScene("" + battleScene);
            foreach (StatSheet enemy in enemyStats)
            {
                BattleManager.Instance.combatants.Add(enemy);
            }
            BattleManager.Instance.combatants.Add(collision.gameObject.GetComponent<StatSheet>());
            BattleManager.Instance.encounterScene = encounterScene;
        }
    }
}
