using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DialogueHandler
{
    private const string RESOURCE_DIALOGUE_PATH = "Prefabs/Dialogues/";
    private DialogueBox dialoguePref;
    private DialogueBox dialogueInst = null;

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

    private List<DialogueItem> dialogueItems = new List<DialogueItem>();
    private bool conDialogue = true;

    private DialogueHandler()
    {
        dialoguePref = Resources.Load<GameObject>("Prefabs/UI/DialogueBox").GetComponent<DialogueBox>();
    }

    public async void Chat(Dialogue dialogue)
    {
        if (dialogue == null)
        {
            return;
        }

        dialogueItems.AddRange(dialogue.dialogueItems);
        foreach (DialogueItem item in dialogueItems)
        {
            if (conDialogue)
            {
                conDialogue = false;
                foreach (Character character in item.characters)
                {
                    //for each message
                    //display on character´s position
                    if (!dialogueInst)
                    {

                        if (GameObject.Find(character.name).TryGetComponent<Character>(out Character characterInstance))
                        {
                            Transform dialoguePoint = null;
                            dialoguePoint = characterInstance.transform.Find("DialoguePoint");
                            if (dialoguePoint != null)
                            {
                                dialogueInst = GameObject.Instantiate<DialogueBox>(dialoguePref, dialoguePoint.position, dialoguePoint.rotation, dialoguePoint);
                            }
                        }
                        else
                        {
                            dialogueInst = GameObject.Instantiate<DialogueBox>(dialoguePref, character.transform.position, character.transform.rotation, character.transform);
                        }
                    }


                    dialogueInst.SetText(item.messages);
                    dialogueInst.OnCompletedDialogue = () =>
                    {
                        conDialogue = true;
                    };
                }

            }

            while (!conDialogue)
            {
                await Task.Delay(100);
            }
        }
        dialogueItems = new List<DialogueItem>();
    }
    public Dialogue GetDialogueObject(string name)
    {
        Dialogue dialogue = Resources.Load<Dialogue>(RESOURCE_DIALOGUE_PATH + name);
        if (!dialogue)
        {
            Debug.LogAssertion("No dialogue were found with " + name + " name.");
        }
        return dialogue;
    }
}
