using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestForHpBar : MonoBehaviour
{
    //hp values
    public float maxHp = 100;
    public float currentHp;

    //the script hpbar
    public HpBar healthBar;


    public Animation hurtAnim;
    Animator animator;

    //selecter
    public GameObject selectGameobject;
    public GameObject selectPos;


    // Start is called before the first frame update
    void Start()
    {
        //set current to max
        currentHp = maxHp;
        healthBar.setMaxHealth(maxHp);

        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //used to test damage
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDamageTake(1.5f);
        }
    }

    public void OnDamageTake(float damage)
    {
        currentHp -= damage;

        //updates the health bar
        healthBar.setHealth(currentHp);

        animator.SetTrigger("hurt");

        if(currentHp <= 0)
        {
            animator.SetTrigger("Die");
        }
    }


    private void OnMouseDown()
    {
        //selects target
        selectGameobject.transform.position = selectPos.transform.position;

        SelectTarget.target = gameObject;
    }
}
