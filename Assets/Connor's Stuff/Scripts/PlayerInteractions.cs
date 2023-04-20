using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    public float interactRange = 1f;

    public bool isTalking = false;
    public bool isTransitioning;
    public PlayerController player;
    public Animator ani;
    public float playerMoveSpeed;

    private DialogueManager dm;
    public Image dialogueBox;
    public Canvas canvas;

    public Canvas canvasPrefab;

    // Start is called before the first frame update
    void Start()
    {
        isTalking = false;
        dm = FindObjectOfType<DialogueManager>();
        dialogueBox = GameObject.Find("Dialogue Box").GetComponent<Image>();

        if (player == null)
            player = gameObject.GetComponent<PlayerController>();

        if(canvas == null)
        {
            canvas = Instantiate(canvasPrefab);
        }

        playerMoveSpeed = player.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (ani == null)
            ani = gameObject.GetComponent<Animator>();

        if (player == null)
            player = gameObject.GetComponent<PlayerController>();



        if (Input.GetKeyDown(KeyCode.E))
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
            if(dialogueBox != null)
            {
                if (dialogueBox.enabled)
                {
                    dialogueBox.enabled = false;
                }
            }
            if(player.moveSpeed != playerMoveSpeed && !isTransitioning)
            {
                player.moveSpeed = playerMoveSpeed;
            }
            player.enabled = true;
        }
        else
        {
            if (!dialogueBox.enabled)
            {
                dialogueBox.enabled = true;
            }
            player.enabled = false;
        }
    }
}
