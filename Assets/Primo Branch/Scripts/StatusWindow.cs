using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusWindow : MonoBehaviour
{
    // Variables

    public StatSheet combatant;
    public GameObject effectIcon;
    public List<GameObject> effectIcons = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (combatant != null)
        {
            foreach (GameObject status in effectIcons)
            {
                Destroy(status);
            }
            effectIcons.Clear();
            StatusEffect[] statuses = combatant.gameObject.GetComponentsInChildren<StatusEffect>();
            Debug.Log(combatant.characterName + " currently has " + statuses.Length + " status effects");
            foreach (StatusEffect status in statuses)
            {
                GameObject effect = Instantiate(effectIcon, transform.position, transform.rotation);
                effect.transform.SetParent(transform.Find("Status Background"));
                effect.transform.GetComponentInChildren<SpriteRenderer>().sprite = status.statusIcon;
                effect.transform.GetComponentInChildren<TextMeshProUGUI>().text = "" + status.currentStacks;
                effectIcons.Add(effect);
            }
        }
    }
}
