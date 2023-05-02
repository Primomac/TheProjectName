using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : StatusEffect
{
    public override void Initialize()
    {
        base.Initialize();
        OnApply = null;
        OnExpire = null;
        OnTick = PainDamage;
        OnPersist = null;

        statusName = "Poison";
        isDebuff = true;
        dispellable = true;
        countByTurn = false;
        stackLimit = 5;
        duration = 10;
        tickTime = 2;
    }

    public void PainDamage(StatSheet stats)
    {
        Debug.Log("Removing " + (currentStacks * 3) + "% of " + stats.name + "'s Max Health by Poison!");
        stats.hp -= stats.maxHp * 0.01f * currentStacks;
    }
}
