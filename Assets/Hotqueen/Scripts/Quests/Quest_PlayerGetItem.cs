using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_PlayerGetItem : Quest
{
    [SerializeField] private Item item;
    public override void StartQuest()
    {
        base.StartQuest();
        Player player = GameObject.FindObjectOfType<Player>();
        if (player.TryGetComponent<InteractionComponent>(out InteractionComponent interactionComponent))
        {
            interactionComponent.OnInteract += Validate;
        }
    }

    private void Validate(GameObject character, GameObject other)
    {
        if (character.TryGetComponent<Player>(out Player player) && player.Inventory.HasItem(item.Data))
        {
            Debug.Log("Player have item");
            if (player.TryGetComponent<InteractionComponent>(out InteractionComponent interactionComponent))
            {
                interactionComponent.OnInteract -= Validate;
            }
            CompleteQuest();
        }
    }
}
