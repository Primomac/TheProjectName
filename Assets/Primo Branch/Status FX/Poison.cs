using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : StatusEffect
{
    public override void Initialize()
    {
        base.Initialize();
        statusIcon = Resources.Load<Sprite>("Status Icons/poison");
        OnApply = null;
        OnExpire = RemoveDebuff;
        OnTick = PainDamage;
        OnPersist = null;

        statusName = "Poison";
        isDebuff = true;
        dispellable = true;
        countByTurn = false;
        stackLimit = 5;
        duration = 10;
        tickTime = 1;
    }

    public void PainDamage(StatSheet stats)
    {
        Debug.Log("Removing " + (currentStacks * 1) + "% of " + stats.name + "'s Max Health by Poison!");
        stats.hp -= Mathf.Ceil(stats.maxHp * 0.01f * currentStacks);
        stats.hpBar.GetComponent<HpBar>().setHealth(stats.hp);
    }

    public void RemoveDebuff(StatSheet stats)
    {
        Destroy(this);
    }
}
