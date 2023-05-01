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
    }

    public void PainDamage(StatSheet stats)
    {
        Debug.Log("Removing " + (currentStacks * 3) + "% of " + stats.name + "'s Max Health by Poison!");
        stats.hp -= stats.maxHp * 0.03f * currentStacks;
    }
}
