using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusView : MonoBehaviour
{
    // Variables

    public List<StatusEffect> currentStatuses = new List<StatusEffect>();
    public StatSheet stats;
    public Canvas canvas;
    public GameObject effectIcon;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (StatusEffect status in stats.gameObject.GetComponents<StatusEffect>())
        {

        }
    }
}
