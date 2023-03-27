using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private TextMeshProUGUI uiDialogue;
    private string dialogueToWrite;
    private float timePerCharacter;
    private int characterIndex;

    private float timer;

    public void AddWriter(TextMeshProUGUI uiDialogue, string dialogueToWrite, float timePerCharacter)
    {
        this.uiDialogue = uiDialogue;
        this.dialogueToWrite = dialogueToWrite;
        this.timePerCharacter = timePerCharacter;

        characterIndex = 0;
    }

    private void Update()
    {
        if(uiDialogue != null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0)
            {
                //Display next character
                timer += timePerCharacter;
                characterIndex++;

                uiDialogue.text = dialogueToWrite.Substring(0, characterIndex);

                if(characterIndex >= dialogueToWrite.Length)
                {
                    //Entire dialogue has been displayed. Stop going
                    uiDialogue = null;
                    return;
                }
            }
        }
    }
}
