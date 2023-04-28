using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Create")]
public class Skill : ScriptableObject
{
    // Variables

    public string skillName;
    public float spCost;
    public string skillDescription;
    public Color skillBackground = new Color(0, 0, 0, 255);
    public List<string> skillSequence = new List<string>();
    public List<AudioClip> sfx = new List<AudioClip>();
    public List<GameObject> effects = new List<GameObject>();

    /* ===== LIST OF SKILL METHODS =====*/
    
    /* 
    When a skill button is pressed on the menu, it will use BattleManager.Instance.gameObject.SendMessage()
    to perform skill methods in the skillSequence list in its given order. The methods will either be stored
    in the BattleManager script or a separate script attached to the BattleManager GameObject made specifically
    to store these methods. DO NOT USE SPACES WHEN WRITING IN METHODS.
    */

    // ApplyStatus(StatusEffect status, StatSheet target) - Applies the [status] status effect to the [target]. Call this multiple times to apply multiple stacks.
    // RemoveStatus(string removeType, int removeAmount, StatSheet target) - Removes [removeAmount] number of status effects from [target] of [removeType], which can equal "Buff", "Debuff", "All", or a specific effect name.
    // DealDamage(float damageMod, string damageType, bool isAOE) - Reduces enemy (or enemies if [isAOE] is true) health by the following formula (offense/defense for ["Physical"], magic/ward for ["Magical"], based on [damageType]): currentCombatant.offense/magic * (100 / (100 + currentTarget.defense/ward)) * damageMod
    // PlayAnimation(string animation) - Plays BattleManager.Instance.currentCombatant's [animation] animation.
    // PlayEffect(GameObject effect, StatSheet target) - Plays a certain animation effect over a specified combatant. [effect] IS ACTUALLY AN INT IN THE CONTEXT OF skillSequence, the BattleManager will get currentSkill.sfx[effect], where [effect] equals the [effect]'s index in the effects list. Also, effects should be 1 in the Combatants sorting layer.
    // PlaySound(AudioClip sound, int repeats) - Plays a specific sound [repeats + 1] times. SOUND IS ACTUALLY AN INT IN THE CONTEXT OF skillSequence, the BattleManager will get currentSkill.sfx[sound], where [sound] equals the sound's index in the sfx list.
    // Restore(string scaleType, string healType, float healMod, StatSheet target) - Restores Health or Spirit (based on [healType]) by an amount dependent on the user's Magic or the [target]'s Max HP/SP (based on [scaleType]) to the [target]. Magic formula: currentCombatant.magic * healMod | HP formula: currentTarget.maxHP * healMod
    // SelectTarget(StatSheet target) - Changes the current target to someone else. Great for "hit random enemy" skills or for combining heals & damage into the same skill.
    // Wait(float time) - Waits for [time] seconds before performing the next method in the sequence. You'll be using this one fairly often, especially for skills involving combatant movement or multiple animations, and anytime sound effects longer than half a second play.
}
