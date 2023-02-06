using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_DeliverItem : Quest
{
    [SerializeField] private Item item;
    [SerializeField] private GameObject target;

    public override void StartQuest()
    {
        if (GameObject.FindObjectOfType<Player>().TryGetComponent<InteractionComponent>(out InteractionComponent component))
        {
            component.OnInteract += ValidateQuest;
        }

        base.StartQuest();
    }

    public override void CompleteQuest()
    {
        base.CompleteQuest();
    }

    public void ValidateQuest(GameObject character, GameObject other)
    {

        if (character == null && other == null)
        {
            return;
        }
        Debug.Log("validating quest " + this.name);
        if (character.TryGetComponent<Player>(out Player player) && player.ItemHolder.GetItem() != null &&
            player.ItemHolder.GetItem().Data.name == item.Data.name && other == this.target)
        {

            player.Inventory.RemoveItem();
            CompleteQuest();
        }
    }
}
