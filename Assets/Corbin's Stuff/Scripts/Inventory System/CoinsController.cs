using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinsController : MonoBehaviour
{
    public static float coinAmount;
    public TextMeshProUGUI coinDisplayAmount;

    // Start is called before the first frame update
    void Start()
    {
        coinAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinDisplayAmount.SetText(coinAmount.ToString());
    }
}
