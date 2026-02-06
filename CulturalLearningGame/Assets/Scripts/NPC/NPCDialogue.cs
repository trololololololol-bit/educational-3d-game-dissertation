using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;


   void Update()
    {
       if(Input.GetKeyDown(KeyCode.E))
        {
           if(dialoguePanel.activeInHierarchy)
            {
                 ZeroText();
             } else
             {
                 dialoguePanel.SetActive(true);
                 StartCoroutine(Typing());
             }
         }

         if(dialogueText.text == dialogue[index])
         {
             contButton.SetActive(true);
        }
     }

   

  



    public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);
        if(index < dialogue.Length -1)
        {
            index++;
            dialogueText.text ="";
            StartCoroutine(Typing());
        } else
        {
            ZeroText();
        }
    }
}
