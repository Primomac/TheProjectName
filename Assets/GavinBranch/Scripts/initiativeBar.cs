using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class initiativeBar : MonoBehaviour
{

    public Slider slider;
    public Image fill;
    public Gradient gradient;


    public void updateInitiativeBar(float initiative)
    {
        slider.value = initiative;

        if(slider.value >= slider.maxValue)
        {
            fill.color = gradient.Evaluate(1);
        }
        else
        {
            fill.color = gradient.Evaluate(0);
        }
    }

    private void Start()
    {
        slider.maxValue = 100;
    }
}
