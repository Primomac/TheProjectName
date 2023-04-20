using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBoxTextActivity : MonoBehaviour
{
    Image dialogueBox;

    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.Find("Dialogue Box").GetComponent<Image>();
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueBox.enabled)
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }

    }
}
