using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDown : StatusEffect
{
    public override void Initialize()
    {
        base.Initialize();
        statusIcon = Resources.Load<Sprite>("Status Icons/speedDown");
        OnApply = DebuffSpeed;
        OnExpire = RemoveBuff;
        OnTick = null;
        OnPersist = DebuffSpeed2;

        statusName = "Speed Down";
        isDebuff = false;
        dispellable = true;
        countByTurn = true;
        stackLimit = 5;
        duration = 3;
    }

    public float baseSpeed;
    public bool startDebuff = false;

    public void DebuffSpeed(StatSheet stats)
    {
        baseSpeed = stats.speed;
        startDebuff = true;
    }

    public void RemoveBuff(StatSheet stats)
    {
        float stacks = currentStacks;
        Debug.Log("Current stack is " + stacks);
        for (float i = 0; i < stacks; i++)
        {
            stats.speed *= 100 / (100 - ((i + 1) * 10));
        }
        Destroy(this);
    }

    public void DebuffSpeed2(StatSheet stats)
    {
        if (startDebuff == true)
        {
            stats.armor -= baseSpeed * 0.10f * currentStacks;
            if (stats.armor < baseSpeed - (baseSpeed * 0.10f * currentStacks))
            {
                stats.armor = baseSpeed - (baseSpeed * 0.10f * currentStacks);
            }
        }
    }
}
