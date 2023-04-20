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
    private int AmountOfEnemysKilled;

    public static int levelsGained;
    public static int Xpleft;
    public static float XpCap;



    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = XpCap;

        AmountOfEnemysKilled = FindEnemySprites.XpYeild.Count;
        for (int i =0; i < AmountOfEnemysKilled; i++)
        {
            xpGained = xpGained + (int)FindEnemySprites.XpYeild[i];
        }
        slider.value = Xpleft;
        Xpleft = 0;
        IncreaseXP();


        //add coins
        for (int i = 0; i < AmountOfEnemysKilled; i++)
        {
            CoinsController.coinAmount = CoinsController.coinAmount + (int)FindEnemySprites.coinYeild[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        xp.text =slider.value + "/" + slider.maxValue;
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

                levelsGained++;
                slider.maxValue = XpCap;
            }

            StartCoroutine(Delay());
        }
        Xpleft = (int)slider.value;
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
