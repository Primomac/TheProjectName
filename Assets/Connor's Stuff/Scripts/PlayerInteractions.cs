using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    public float interactRange = 1f;

    [HideInInspector]
    public bool isTalking = false;

    private DialogueManager dm;
    public Image dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        isTalking = false;
        dm = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);
            foreach(Collider2D collider in colliderArray)
            {
                if(collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    if (!isTalking)
                    {
                        npcInteractable.InitiateDialogue();
                        isTalking = true;
                    }
                    else
                    {
                        dm.DisplayNextSentence();
                    }
                }
                Debug.Log(collider);
            }
            
        }

        if(isTalking && !dialogueBox.gameObject.activeSelf)
        {
            dialogueBox.gameObject.SetActive(true);
        }
        if(!isTalking && dialogueBox.gameObject.activeSelf)
        {
            dialogueBox.gameObject.SetActive(false);
        }
    }
}
