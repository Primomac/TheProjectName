using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndSceneStats : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int stat;

    // Start is called before the first frame update
    void Start()
    {
        if(stat == 1)
        {
            stat = XpGain.NumberOfEnemiesKilled;
        }
        if(stat == 2)
        {
            stat = XpGain.NumberOfCoinsClaimed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + stat;
    }
}
