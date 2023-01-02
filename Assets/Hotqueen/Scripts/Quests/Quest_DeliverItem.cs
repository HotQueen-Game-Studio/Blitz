using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_DeliverItem : Quest
{
    [SerializeField] private Item item;
    [SerializeField] private InteractionComponent character;
    [SerializeField] private GameObject target;

    public override void StartQuest()
    {
        DialogueHandler.Instance.Chat(DialogueHandler.Instance.GetDialogueObject("StartedMachinePuzzle"));
        character.OnInteract += ValidateQuest;
        base.StartQuest();
    }

    public override void CompleteQuest()
    {
        DialogueHandler.Instance.Chat(DialogueHandler.Instance.GetDialogueObject("FinishedMachinePuzzle"));
        base.CompleteQuest();
    }

    public void ValidateQuest(GameObject character, GameObject other)
    {
        if (character == null || other == null)
        {
            return;
        }

        if (character.TryGetComponent<Player>(out Player player) && player.ItemHolder.GetItem() != null &&
            player.ItemHolder.GetItem().Data.name == item.Data.name && other == this.target)
        {
            player.Inventory.RemoveItem();
            CompleteQuest();
        }
    }
}
