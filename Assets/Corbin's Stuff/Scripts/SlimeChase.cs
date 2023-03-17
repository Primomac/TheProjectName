using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeChase : MonoBehaviour
{
    public Transform player;
    public float jump;
    public float jumpTime;

    float distance;
    public float distanceToFollow;

    public bool isFollowing;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;


        if (distance < distanceToFollow)
        {
            StartCoroutine(Jump());
            isFollowing = true;
        }
        if(distance > distanceToFollow)
        {
            StopCoroutine(Jump());
            isFollowing = false;
        }
    }

    IEnumerator Jump()
    {
        rb.AddForce(Vector2.MoveTowards(this.transform.position, player.position, jump), ForceMode2D.Impulse);
        yield return new WaitForSeconds(jumpTime);
    }    
}
