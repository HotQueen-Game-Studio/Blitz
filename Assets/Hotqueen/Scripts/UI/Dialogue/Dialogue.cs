using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Create New Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueItem[] dialogueItems = null;
}

