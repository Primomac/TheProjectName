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
    private float currentXpGained;
    private int AmountOfEnemysKilled;
    private int AmountOfLoops = 0;

    public static int levelsGained;
    public static int Xpleft;
    public static float XpCap = 100;
    public static int NumberOfEnemiesKilled;

    public StatSheet player;

    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < levelsGained; i++)
        {
            slider.maxValue += Mathf.Round(slider.maxValue * 0.45f);
        }

        AmountOfEnemysKilled = FindEnemySprites.XpYeild.Count;
        for (int i =0; i < AmountOfEnemysKilled; i++)
        {
            xpGained = xpGained + (int)FindEnemySprites.XpYeild[i];

            //add coins
            CoinsController.coinAmount = CoinsController.coinAmount + (int)FindEnemySprites.coinYeild[i];

            NumberOfEnemiesKilled++;
        }
        currentXpGained = Xpleft;
        Xpleft = 0;
        IncreaseXP();
    }

    // Update is called once per frame
    void Update()
    {
        xp.text =slider.value + "/" + slider.maxValue;
        slider.value = currentXpGained;
    }

    public void IncreaseXP()
    {
        if(AmountOfLoops != 100)
        {
            float onePercentOfInt = (float)xpGained * 0.01f;
            currentXpGained += onePercentOfInt;

            AmountOfLoops++;

            //levelUp
            if (currentXpGained >= slider.maxValue)
            {
                currentXpGained -= slider.maxValue;
                slider.maxValue += Mathf.Round(slider.maxValue * 0.45f);
                levelsGained++;
            }

            StartCoroutine(Delay());
        }
        Xpleft = (int)slider.value;
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.01f);
        IncreaseXP();
    }
    public void ResetXp()
    {
        xpGained = 0;
        FindEnemySprites.enemySprite.Clear();
        FindEnemySprites.XpYeild.Clear();
        
    }
}
