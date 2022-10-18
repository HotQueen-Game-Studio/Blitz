using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler
{
    private DialogueBox dialoguePref;

    private static DialogueHandler instance = null;
    public static DialogueHandler Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("new instance");
                instance = new DialogueHandler();
            }
            return instance;
        }
    }

    private DialogueHandler()
    {
        dialoguePref = Resources.Load<GameObject>("Prefabs/UI/DialogueBox").GetComponent<DialogueBox>();
    }

    public void Chat(Character character, string[] messages)
    {
        //for each message
        //display on character´s position
        DialogueBox dialogueInst = character.GetComponentInChildren<DialogueBox>();
        Transform dialoguePoint = character.transform.Find("DialoguePoint");
        
        if (!dialogueInst)
        {
            if (dialoguePoint != null)
            {
                dialogueInst = GameObject.Instantiate<DialogueBox>(dialoguePref, dialoguePoint.position, dialoguePoint.rotation, dialoguePoint);
            }
            else
            {
                dialogueInst = GameObject.Instantiate<DialogueBox>(dialoguePref, character.transform.position, character.transform.rotation, character.transform);
            }
        }

        dialogueInst.SetText(messages);
    }

    public void Chat(Character character, string message)
    {
        string[] s = { message };
        Chat(character, s);
    }
}
