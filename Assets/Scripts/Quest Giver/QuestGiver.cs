using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class QuestGiver : MonoBehaviour
{
    private UnityEngine.UI.Image DialogueImage;
    private TMP_Text DialogueText;
    private GameObject Player;
    private PlayerMovement PlayerScript;
    private GameObject DialogueFieldObject;
    private GameObject DialogueTextObject;
    public void GenerateDialogue()
    {
        DialogueFieldObject = GameObject.Find("DialogueField");
        DialogueTextObject = GameObject.Find("DialogueText");
        DialogueImage = DialogueFieldObject.GetComponent<UnityEngine.UI.Image>();
        DialogueText = DialogueTextObject.GetComponent<TMP_Text>();
        DialogueImage.enabled = true;
        DialogueText.enabled = true;
        GetResponse();
    }

    private void GetResponse()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerMovement>();
        // Update the text field with the response
        if (PlayerScript.hasEscortedAdventurer == false)
        {
            DialogueText.text = "An adventurer just like you went to go explore in that dungeon behind me to the left there and he hasn't come back for a few hours. Can you go in and escort him out? We'd be really grateful!";
        }
        if (PlayerScript.hasEscortedAdventurer == true)
        {
            DialogueText.text = "You've brought the adventurer back safely! He was just talking to me saying how he probably would not have been able to get out if it weren't for you. We'll forever be in your debt fellow adventurer!";
        }
        Invoke("CloseDialogue", 10);

    }

    private void CloseDialogue()
    {
        DialogueImage.enabled = false;
        DialogueText.enabled = false;
    }
}
