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
    public float distanceToStop;

    public static bool slimeIsFollowing;
    Vector2 direction;

    bool followCoroutineActive;

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
        direction = player.transform.position - transform.position;

        if (distance < distanceToFollow)
        {
            if(!followCoroutineActive)
            {
                followCoroutineActive = true;
                StartCoroutine(Jump());
            }

            slimeIsFollowing = true;
        }
        if(distance > distanceToStop)
        {
            slimeIsFollowing = false;
            StopCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        rb.AddForce(direction.normalized * 2 * jump, ForceMode2D.Impulse);
        Debug.Log("jumped");
        yield return new WaitForSecondsRealtime(jumpTime);
        followCoroutineActive = false;
    }    
}
