using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemySprites : MonoBehaviour
{
    public static List<Sprite> enemySprite = new List<Sprite>();
    public static List<float> XpYeild = new List<float>();
 
    public void findEnemySprites()
    {
        GameObject[] numberOfEnemys;
        numberOfEnemys = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in numberOfEnemys)
        {
            enemySprite.Add(enemy.gameObject.GetComponent<SpriteRenderer>().sprite);
            XpYeild.Add(enemy.gameObject.GetComponent<StatSheet>().expYield);
        }
    }
}
