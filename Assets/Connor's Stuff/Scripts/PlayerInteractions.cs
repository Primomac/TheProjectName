using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    public float interactRange = 1f;

    [HideInInspector]
    public bool isTalking = false;
    public bool isTransitioning;
    public PlayerController player;
    public Animator ani;
    public float playerMoveSpeed;

    private DialogueManager dm;
    public Image dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        isTalking = false;
        dm = FindObjectOfType<DialogueManager>();

        ani = gameObject.GetComponent<Animator>();
        player = gameObject.GetComponent<PlayerController>();
        playerMoveSpeed = player.moveSpeed;
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

        if(isTransitioning)
        {
            if(!dialogueBox.gameObject.activeSelf && isTalking)
            {
                dialogueBox.gameObject.SetActive(true);
            }
            if(player.moveSpeed != 0f)
            {
                player.moveSpeed = 0f;
            }
            ani.SetFloat("horiInput", 0f);
            ani.SetFloat("vertInput", 0f);
            player.enabled = false;
        }
        if(!isTalking)
        {
            if(dialogueBox.gameObject.activeSelf)
            {
                dialogueBox.gameObject.SetActive(false);
            }
            if(player.moveSpeed != playerMoveSpeed && !isTransitioning)
            {
                player.moveSpeed = playerMoveSpeed;
            }
            player.enabled = true;
        }
    }
}
