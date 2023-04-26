using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentStatStorage : MonoBehaviour
{
    public int[] EquipmentStatsArray;
    public GameObject EM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeStats()
    {
        EquipmentStatsArray[0] = 0;
        foreach(Item item in EM.GetComponent<EquipManager>().Items)
        {
            EquipmentStatsArray[0] = EquipmentStatsArray[0] + item.value;
        }
    }
}
