using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseDown : StatusEffect
{
    public override void Initialize()
    {
        base.Initialize();
        OnApply = DebuffDefense;
        OnExpire = RemoveDebuff;
        OnTick = null;
        OnPersist = DebuffDefense2;

        statusName = "Armor Down";
        isDebuff = false;
        dispellable = true;
        countByTurn = true;
        stackLimit = 5;
        duration = 3;
    }

    public float baseDefense;
    public bool startDebuff = false;

    public void DebuffDefense(StatSheet stats)
    {
        baseDefense = stats.armor;
        startDebuff = true;
    }

    public void RemoveDebuff(StatSheet stats)
    {
        startDebuff = false;
        float stacks = currentStacks;
        Debug.Log("Current stack is " + stacks);
        for (float i = 0; i < stacks; i++)
        {
            stats.armor *= 100 / (100 - ((i + 1) * 10));
        }
        Destroy(this);
    }

    public void DebuffDefense2(StatSheet stats)
    {
        if (startDebuff == true)
        {
            stats.armor -= baseDefense * 0.10f * currentStacks;
            if (stats.armor < baseDefense - (baseDefense * 0.10f * currentStacks))
            {
                stats.armor = baseDefense - (baseDefense * 0.10f * currentStacks);
            }
        }
    }
}
