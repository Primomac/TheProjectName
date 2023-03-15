using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables

    public float moveSpeed;

    public Rigidbody2D rig;
    public BoxCollider2D box;
    public Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(moveSpeed * Time.deltaTime * horiInput, moveSpeed * Time.deltaTime * vertInput));   

    }
}
