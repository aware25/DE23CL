using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    public int index;

    public GameObject contButton;
    public GameObject startButton;
    public float wordSpeed;
    public bool playerIsClose;

    public static bool PausedHere = false;

    public static string StringToParse;

    //public static GameObject PlayerSpeech = GameObject.Find("DawnSpeech");



    void Update()
    {    

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
        
    }
    
    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;

        if (GameObject.Find("DawnSpeech").activeInHierarchy == true)
        {
            GameObject.Find("DawnSpeech").SetActive(false);
            
        }
        

    }

    IEnumerator Typing()
    {
        PausedHere = true;


        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void startLine(string StringToParse)
    {
        dialogueText.text +=StringToParse;
        startButton.SetActive(false);
        StartCoroutine(Typing());
        NextLine();
    }

    public void NextLine()
    {

        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
