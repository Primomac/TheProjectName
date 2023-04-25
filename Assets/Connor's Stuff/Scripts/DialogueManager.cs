using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private PlayerInteractions playerInteractions;
    private Queue<string> sentences;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    [SerializeField] private TextWriter dialogueWriter;
    public float timePerCharacter;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        playerInteractions = FindObjectOfType<PlayerInteractions>();
        dialogueText = GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        nameText = GameObject.Find("NPC Name").GetComponent<TextMeshProUGUI>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        if(sentences != null)
            sentences.Clear();
        
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        nameText.text = dialogue.name;
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = null;
        dialogueWriter.AddWriter(dialogueText, sentence, timePerCharacter);
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");
        playerInteractions.isTalking = false;
    }
}
