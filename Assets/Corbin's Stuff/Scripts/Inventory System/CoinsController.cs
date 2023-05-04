using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinsController : MonoBehaviour
{
    public static int coinAmount;
    public TextMeshProUGUI coinDisplayAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        coinDisplayAmount.SetText(coinAmount.ToString());

        if(SceneManager.GetActiveScene().name == "Title Scene")
        {
            coinDisplayAmount.enabled = true;
        }
        else
        {
            coinDisplayAmount.enabled = true;
        }
    }
}
