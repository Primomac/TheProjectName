using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardDown : StatusEffect
{
    public override void Initialize()
    {
        base.Initialize();
        OnApply = DebuffDefense;
        OnExpire = RemoveDebuff;
        OnTick = null;
        OnPersist = DebuffDefense2;

        statusName = "Ward Down";
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
        baseDefense = stats.ward;
        startDebuff = true;
    }

    public void RemoveDebuff(StatSheet stats)
    {
        startDebuff = false;
        float stacks = currentStacks;
        Debug.Log("Current stack is " + stacks);
        for (float i = 0; i < stacks; i++)
        {
            stats.ward *= 100 / (100 - ((i + 1) * 10));
        }
        Destroy(this);
    }

    public void DebuffDefense2(StatSheet stats)
    {
        if (startDebuff == true)
        {
            stats.ward -= baseDefense * 0.10f * currentStacks;
            if (stats.ward < baseDefense - (baseDefense * 0.10f * currentStacks))
            {
                stats.ward = baseDefense - (baseDefense * 0.10f * currentStacks);
            }
        }
    }
}
