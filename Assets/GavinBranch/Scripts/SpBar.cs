using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpBar : MonoBehaviour
{
    public Image fill;
    public TextMeshProUGUI SpText;
    public float MaxSp;


    public void updateSpBar(float Sp)
    {
        fill.fillAmount = Sp/MaxSp;

        SpText.text = Mathf.Round(Sp) + "/" + MaxSp;
    }

    public void SetMaxSp(float MaxSpAmount)
    {
        MaxSp = MaxSpAmount;
    }
}
