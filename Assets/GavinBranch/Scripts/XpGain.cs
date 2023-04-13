using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XpGain : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI xp;
    public static int xpGained;
    private int currentXpGained;
    private int AmountOfEnemysKilled;
    // Start is called before the first frame update
    void Start()
    {
        AmountOfEnemysKilled = FindEnemySprites.XpYeild.Count;
        for (int i =0; i < AmountOfEnemysKilled; i++)
        {
            xpGained = xpGained + (int)FindEnemySprites.XpYeild[i];
        }

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
            if(slider.value >= slider.maxValue)
            {
                slider.value = 0;
            }

            StartCoroutine(Delay());
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.1f);
        IncreaseXP();
    }
    public void ResetXp()
    {
        xpGained = 0;
        FindEnemySprites.enemySprite.Clear();
        FindEnemySprites.XpYeild.Clear();
        
    }
}
