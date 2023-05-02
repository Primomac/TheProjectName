using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEquipManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EquipManager[] ems = GameObject.FindObjectsOfType<EquipManager>();
        foreach(EquipManager em in ems)
        {
            Debug.Log(em.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
