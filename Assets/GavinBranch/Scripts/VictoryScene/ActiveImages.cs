using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveImages : MonoBehaviour
{
    public GameObject[] BountyPaper;
    private int numberOfEnemys;
    // Start is called before the first frame update
    void Start()
    {
        numberOfEnemys = FindEnemySprites.enemySprite.Count;
        Debug.Log(numberOfEnemys);

        for(int i=0; i < numberOfEnemys; i++)
        {
            BountyPaper[i].SetActive(true);
        }
    }
}
