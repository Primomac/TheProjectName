using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertEnemyPull : MonoBehaviour
{
    float distance;
    public float distanceToPull;
    float originalPull;
    public float pullForce;
    public float pullMultiplierWhenClose;

    Vector2 direction;
    public Transform player;

    bool isTriggered;


    // Start is called before the first frame update
    void Start()
    {
        originalPull = pullForce;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);
        direction = player.position - transform.position;

        if(distance < distanceToPull)
        {
            player.GetComponent<Rigidbody2D>().AddForce(-direction.normalized * pullForce);
            if (distance < distanceToPull * .5 && !isTriggered)
            {
                pullForce *= pullMultiplierWhenClose;
                isTriggered = true;
            }
            if(distance > distanceToPull * .5)
            {
                isTriggered = false;
                pullForce = originalPull;
            }
        }
    }
}
