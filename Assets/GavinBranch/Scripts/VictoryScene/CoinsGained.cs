using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsGained : MonoBehaviour
{
    public int ID;
    private TextMeshProUGUI mytextcomponent;
    // Start is called before the first frame update
    void Start()
    {
        mytextcomponent = this.GetComponent<TextMeshProUGUI>();
        mytextcomponent.text = "+" + FindEnemySprites.coinYeild[ID];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
