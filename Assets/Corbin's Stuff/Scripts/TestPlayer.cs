using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bc;
    public float walkspeed;
    float speedLimiter = .7f;
    float horizontalInput;
    float verticalInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        rb.gravityScale = 0.0f;

        if(Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("meow");
        }

    }

    private void FixedUpdate()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (horizontalInput != 0 && verticalInput != 0)
            {
                horizontalInput *= speedLimiter;
                verticalInput *= speedLimiter;
            }

            rb.velocity = new Vector2(horizontalInput * walkspeed, verticalInput * walkspeed);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
