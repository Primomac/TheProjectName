using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatusEffect : MonoBehaviour
{
    // Variables

    public string statusName;    // Name of the status.
    public Sprite statusIcon;    // How the status will display under the combatant.
    public bool isDebuff;        // If false, it is considered a buff.
    public bool dispellable;     // If true, the effect can be removed by the RemoveEffect() function in a skill's skillSequence.
    public bool countByTurn;     // If false, the value of duration is the status's lifetime in seconds. If true, duration represents turns instead.
    public int stackLimit = 1; // How many times it can stack. If it can stack more than once, its effect will likely be applied more than once.
    public int currentStacks;  // The current amount of stacks.
    public float duration;       // This effect will last for X seconds/turns (based on countByTurn). If 0/null, will not disappear until stacks are dispelled or consumed. If everyone is tied for speed, it will take around 2.85 seconds for them to take their turns.
    public float tickTime;       // After X seconds have passed, call OnTick().
    
    public delegate void EffectType(StatSheet stats);
    public EffectType OnApply;   // OnApply occurs as soon as the status effect is added to the combatant.
    public EffectType OnExpire;  // OnExpire occurs when the status effect is dispelled or its duration ends. MAKE SURE TO DESTROY THE STATUS IN THIS METHOD, IT IS REQUIRED TO END THE EFFECT.
    public EffectType OnTick;    // OnTick will occur every tickTime seconds, starting from the moment the status effect is applied.
    public EffectType OnPersist; // OnPersist will be effective for the entire duration of the status effect. MAKE SURE STAT MODS CLAMP THEIR VALUES.
    /*
    public UnityEvent EffectApply;
    public UnityEvent EffectExpire;
    public UnityEvent EffectTick;
    public UnityEvent EffectPersist;
    */
    public float timeTillTrigger;
    public float timeTillExpire;

    // Awake is called when the object is loaded for the first time
    void Awake()
    {
        Initialize();
        currentStacks++;
        if (OnApply != null)
        {
            //OnApply(gameObject.GetComponent<StatSheet>());
        }
        timeTillTrigger = tickTime;
        timeTillExpire = duration;
    }

    // Update is called once per frame
    void Update()
    {   
        if (OnPersist != null)  // Takes effect while the StatSheet exists
        {
            OnPersist(GetComponent<StatSheet>());
        }
        if (OnTick != null && GameObject.Find("Battle Manager").GetComponent<BattleManager>().tickInitiative == true)   // Ignored if nothing happens over time
        {
            timeTillTrigger -= Time.deltaTime;
            if (!countByTurn)
            {
                timeTillExpire -= Time.deltaTime;
            }
            if (timeTillTrigger <= 0 && OnTick != null)
            {
                OnTick(gameObject.GetComponent<StatSheet>());
                timeTillTrigger = tickTime;
            }
            if (timeTillExpire <= 0)
            {
                OnExpire(gameObject.GetComponent<StatSheet>());
            }
        }
    }

    public virtual void Initialize()
    {
        // Set code for changing EffectTypes here
    }
}
