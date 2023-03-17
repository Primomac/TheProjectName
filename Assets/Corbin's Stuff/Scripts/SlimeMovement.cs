using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float xMovement;
    public float yMovement;

    public bool isMovingOnX;
    public bool isMovingOnY;

    float timeLeftX;
    float timeLeftY;

    [Header("Double timeToReverse for 2 jumps per cycle, triple for 3, etc.")]
    public float timeToReverse = 2f;
    public float repeatTime = 2f;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        bool isFollowing = GetComponent<SlimeChase>().isFollowing;

        if(!isFollowing)
        {
            rb = GetComponent<Rigidbody2D>();

            if (isMovingOnX)
            {
                InvokeRepeating("JumpX", 0, repeatTime);
            }

            if (isMovingOnY)
            {
                InvokeRepeating("JumpY", 0, repeatTime);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftX -= Time.deltaTime;
        timeLeftY -= Time.deltaTime;

        bool isFollowing = GetComponent<SlimeChase>().isFollowing;

        if(!isFollowing)
        {
            if (timeLeftX <= 0)
            {
                xMovement *= -1;
                timeLeftX += timeToReverse;
            }

            if (timeLeftY <= 0)
            {
                yMovement *= -1;
                timeLeftY += timeToReverse;
            }
        }
    }

   void JumpX()
    {
        rb.AddForce(Vector2.right * xMovement, ForceMode2D.Impulse);
    }

    void JumpY()
    {
        rb.AddForce(Vector2.up * yMovement, ForceMode2D.Impulse);
    }
}
