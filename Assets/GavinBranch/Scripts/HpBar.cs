using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(int health)
    {
        slider.value = health;

        //updates color for health bar
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        //set color for healthbar
        fill.color = gradient.Evaluate(1);
    }
}
