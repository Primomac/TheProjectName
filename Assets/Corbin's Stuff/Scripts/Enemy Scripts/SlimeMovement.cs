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

    public float timeToReverse = 2f;
    public float repeatTime = 2f;

    bool xInvokeIsActive;
    bool yInvokeIsActive;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftX -= Time.deltaTime;
        timeLeftY -= Time.deltaTime;

        if(!SlimeChase.slimeIsFollowing)
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

            
            if (isMovingOnX && !xInvokeIsActive)
            {
                xInvokeIsActive = true;
                StartCoroutine(JumpX());
            }

            if (isMovingOnY && !yInvokeIsActive)
            {
                yInvokeIsActive = true;
                StartCoroutine(JumpY());
            }
        }
    }

    IEnumerator JumpX()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.right * xMovement, ForceMode2D.Impulse);
        yield return new WaitForSeconds(repeatTime);
        xInvokeIsActive = false;
    }

    IEnumerator JumpY()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * yMovement, ForceMode2D.Impulse);
        yield return new WaitForSeconds(repeatTime);
        yInvokeIsActive = false;
    }
}
