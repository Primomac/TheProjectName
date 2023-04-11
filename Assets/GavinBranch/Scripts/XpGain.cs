using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XpGain : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI xp;
    public int xpGained;
    private int currentXpGained;
    // Start is called before the first frame update
    void Start()
    {
        IncreaseXP();
    }

    // Update is called once per frame
    void Update()
    {
        xp.text =slider.value + "/100";
    }

    public void IncreaseXP()
    {
        if(xpGained != currentXpGained)
        {
            currentXpGained++;
            slider.value++;
            StartCoroutine(Delay());
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.1f);
        IncreaseXP();
    }
}
