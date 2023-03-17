using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public Gradient gradient;

    public TextMeshProUGUI TextOnHpBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(float health)
    {
        slider.value = health;

        //updates color for health bar
        fill.color = gradient.Evaluate(slider.normalizedValue);

        TextOnHpBar.text = slider.value + "/" + slider.maxValue;
    }
    public void setMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        //set color for healthbar
        fill.color = gradient.Evaluate(1);

        TextOnHpBar.text = slider.value + "/" + slider.maxValue;
    }
}
