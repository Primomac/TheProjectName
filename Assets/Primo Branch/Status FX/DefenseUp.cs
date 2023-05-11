using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUp : StatusEffect
{
    public override void Initialize()
    {
        base.Initialize();
        statusIcon = Resources.Load<Sprite>("Status Icons/attackUp");
        OnApply = BuffDefense;
        OnExpire = RemoveBuff;
        OnTick = null;
        OnPersist = BuffDefense2;

        statusName = "Defense Up";
        isDebuff = false;
        dispellable = true;
        countByTurn = true;
        stackLimit = 5;
        duration = 3;
    }

    public float baseArmor;
    public bool startBuff = false;

    public void BuffDefense(StatSheet stats)
    {
        baseArmor = stats.armor;
        startBuff = true;
    }

    public void RemoveBuff(StatSheet stats)
    {
        float stacks = currentStacks;
        Debug.Log("Current stack is " + stacks);
        for (float i = 0; i < stacks; i++)
        {
            stats.armor *= 100 / (100 + ((15 * stacks) - (i * 15)));
        }
        Destroy(this);
    }

    public void BuffDefense2(StatSheet stats)
    {
        if (startBuff == true)
        {
            stats.armor += baseArmor * 0.15f * currentStacks;
            if (stats.armor > baseArmor * (1 + 0.15f * currentStacks))
            {
                stats.armor = baseArmor * (1 + 0.15f * currentStacks);
            }
        }
    }
}
