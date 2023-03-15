using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestForHpBar : MonoBehaviour
{
    //hp values
    public int maxHp = 100;
    public int currentHp;

    //the script hpbar
    public HpBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        //set current to max
        currentHp = maxHp;
        healthBar.setMaxHealth(maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        //used to test damage
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDamageTake(20);
        }
    }

    public void OnDamageTake(int damage)
    {
        currentHp -= damage;

        //updates the health bar
        healthBar.setHealth(currentHp);
    }
}
