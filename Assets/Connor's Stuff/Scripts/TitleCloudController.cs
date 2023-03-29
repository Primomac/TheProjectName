using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCloudController : MonoBehaviour
{
    public float moveSpeed;

    public float xLimit;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        if(transform.position.x < xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, 9.5f);
        }
    }
}
