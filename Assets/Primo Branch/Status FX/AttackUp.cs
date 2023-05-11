using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : StatusEffect
{
    public override void Initialize()
    {
        base.Initialize();
        statusIcon = Resources.Load<Sprite>("Status Icons/attackUp");
        OnApply = BuffAttack;
        OnExpire = RemoveBuff;
        OnTick = null;
        OnPersist = BuffAttack2;

        statusName = "Attack Up";
        isDebuff = false;
        dispellable = true;
        countByTurn = true;
        stackLimit = 5;
        duration = 3;
    }

    public float baseAttack;
    public bool startBuff = false;

    public void BuffAttack(StatSheet stats)
    {
        baseAttack = stats.offense;
        startBuff = true;
    }

    public void RemoveBuff(StatSheet stats)
    {
        float stacks = currentStacks;
        Debug.Log("Current stack is " + stacks);
        for (float i = 0; i < stacks; i++)
        {
            stats.offense *= 100 / (100 + ((15 * stacks) - (i * 15)));
        }
        Destroy(this);
    }

    public void BuffAttack2(StatSheet stats)
    {
        if (startBuff == true)
        {
            stats.offense += baseAttack * 0.15f * currentStacks;
            if (stats.offense > baseAttack * (1 + 0.15f * currentStacks))
            {
                stats.offense = baseAttack * (1 + 0.15f * currentStacks);
            }
        }
    }
}
