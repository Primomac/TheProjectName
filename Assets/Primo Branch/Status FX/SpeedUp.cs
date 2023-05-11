using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : StatusEffect
{
    public override void Initialize()
    {
        base.Initialize();
        statusIcon = Resources.Load<Sprite>("Status Icons/speedUp");
        OnApply = BuffSpeed;
        OnExpire = RemoveBuff;
        OnTick = null;
        OnPersist = BuffSpeed2;

        statusName = "Speed Up";
        isDebuff = false;
        dispellable = true;
        countByTurn = true;
        stackLimit = 5;
        duration = 3;
    }

    public float baseSpeed;
    public bool startBuff = false;

    public void BuffSpeed(StatSheet stats)
    {
        baseSpeed = stats.speed;
        startBuff = true;
    }

    public void RemoveBuff(StatSheet stats)
    {
        float stacks = currentStacks;
        Debug.Log("Current stack is " + stacks);
        for (float i = 0; i < stacks; i++)
        {
            stats.speed *= 100 / (100 + ((15 * stacks) - (i * 15)));
        }
        Destroy(this);
    }

    public void BuffSpeed2(StatSheet stats)
    {
        if (startBuff == true)
        {
            stats.speed += baseSpeed * 0.15f * currentStacks;
            if (stats.speed > baseSpeed * (1 + 0.15f * currentStacks))
            {
                stats.speed = baseSpeed * (1 + 0.15f * currentStacks);
            }
        }
    }
}
