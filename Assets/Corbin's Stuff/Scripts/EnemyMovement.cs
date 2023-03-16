using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float xMovement;
    public float yMovement;

    public bool isMovingOnX;
    public bool isMovingOnY;

    public float timeToReverse = 2f;
    float timeLeftX;
    float timeLeftY;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftX -= Time.deltaTime;
        timeLeftY -= Time.deltaTime;


        bool isFollowing = GetComponent<EnemyChase>().isFollowing;

        if(!isFollowing)
        {
            if (isMovingOnX)
            {
                transform.Translate(Vector2.right * xMovement * Time.deltaTime);
                if (timeLeftX <= 0)
                {
                    xMovement *= -1;
                    timeLeftX += timeToReverse;
                }
            }
            if (isMovingOnY)
            {
                transform.Translate(Vector2.up * yMovement * Time.deltaTime);
                if (timeLeftY <= 0)
                {
                    yMovement *= -1;
                    timeLeftY = +timeToReverse;
                }
            }
        }
    }
}
