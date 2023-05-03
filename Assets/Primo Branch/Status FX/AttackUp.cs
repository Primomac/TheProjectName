using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : StatusEffect
{
    public override void Initialize()
    {
        base.Initialize();
        OnApply = BuffAttack;
        OnExpire = RemoveBuff;
        OnTick = null;
        OnPersist = BuffAttack;

        statusName = "Attack Up";
        isDebuff = false;
        dispellable = true;
        countByTurn = true;
        stackLimit = 5;
        duration = 3;
    }

    public float baseAttack;

    public void BuffAttack(StatSheet stats)
    {
        baseAttack = stats.offense;
        stats.offense += stats.offense * 0.15f * currentStacks;
    }

    public void RemoveBuff(StatSheet stats)
    {
        //stats.offense = baseAttack;
        stats.offense *= baseAttack / stats.offense;
    }
}
