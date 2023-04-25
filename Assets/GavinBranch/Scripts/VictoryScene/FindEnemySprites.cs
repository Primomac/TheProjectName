using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemySprites : MonoBehaviour
{
    public static List<Sprite> enemySprite = new List<Sprite>();
    public static List<float> XpYeild = new List<float>();
    public static List<float> coinYeild = new List<float>();
 
    public void findEnemySprites()
    {
        List<GameObject> numberOfEnemys = new List<GameObject>();

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.GetComponent<StatSheet>())
            {
                numberOfEnemys.Add(enemy);
            }
        }



        foreach (GameObject enemy in numberOfEnemys)
        {
            enemySprite.Add(enemy.gameObject.GetComponent<SpriteRenderer>().sprite);
            XpYeild.Add(enemy.gameObject.GetComponent<StatSheet>().expYield);
            coinYeild.Add(enemy.gameObject.GetComponent<StatSheet>().coinYield);
        }
    }
}
