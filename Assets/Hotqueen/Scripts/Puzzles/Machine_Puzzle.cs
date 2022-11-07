using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Puzzle : Puzzle
{
    [SerializeField] private Item key;
    [SerializeField] private GameObject reward;

    public Machine_Puzzle(Item key, GameObject reward)
    {
        this.key = key;
        this.reward = reward;
    }

    public override void Completed()
    {
        if (!completed)
        {
            completed = true;
            // GameObject.Instantiate<GameObject>(reward, new Vector3(), Quaternion.identity);
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            DialogueHandler.Instance.Chat(player, "Puzzle Completed");
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
                DialogueHandler.Instance.Chat(player, "Maybe it´s the wrong item");
            }
        }
        else if (extraInfo.gameObject.TryGetComponent<Machine>(out Machine machine))
        {
            if (machine.Resistance <= 0)
            {
                Completed();
                return true;
            }
        }

        return false;
    }
}
