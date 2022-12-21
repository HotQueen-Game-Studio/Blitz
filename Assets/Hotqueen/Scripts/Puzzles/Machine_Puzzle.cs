using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Puzzle : Puzzle
{
    [SerializeField] private Item key;
    [SerializeField] private SimpleDoor door;
    [SerializeField] private Silhouette silhouette;

    public Machine_Puzzle(Item key, SimpleDoor door, Silhouette silhouette)
    {
        this.key = key;
        this.door = door;
        this.silhouette = silhouette;
    }

    public override void Completed()
    {
        if (!completed)
        {
            completed = true;
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            if (door)
            {
                door.Locked = false;
            }
            else
            {
                Debug.LogAssertion("Door was not found.");
            }

            DialogueHandler.Instance.Chat(DialogueHandler.Instance.GetDialogueObject("FinishedMachinePuzzle"));
        }
    }

    public override void Started()
    {
    }

    public override bool Validate<T>(T extraInfo)
    {
        if (extraInfo.gameObject.TryGetComponent<Player>(out Player player))
        {
            Item item = player.ItemHolder.GetItem();
            if (item && item.Data.name == key.Data.name)
            {
                player.Inventory.RemoveItem();
                Completed();
                return true;
            }
            else
            {
                // DialogueHandler.Instance.Chat(player, "Maybe it´s the wrong item");
            }
        }
        else if (extraInfo.gameObject.TryGetComponent<Machine>(out Machine machine))
        {
            if (machine.Resistance <= 0)
            {
                silhouette.twisted = true;
                Completed();
                return true;
            }
        }

        return false;
    }
}
