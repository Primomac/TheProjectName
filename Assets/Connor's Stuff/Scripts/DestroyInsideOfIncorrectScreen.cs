using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyInsideOfIncorrectScreen : MonoBehaviour
{
    string sceneTheGameobjectStartsIn;

    // Start is called before the first frame update
    void Start()
    {
        sceneTheGameobjectStartsIn = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (sceneTheGameobjectStartsIn != SceneManager.GetActiveScene().name)
        {
            Destroy(gameObject);
        }
    }
}
