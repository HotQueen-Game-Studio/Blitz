using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail_Puzzle : Puzzle
{
    [SerializeField] private Item mail;
    [SerializeField] private Item deadBird;
    // [SerializeField] private Item reward;

    public Mail_Puzzle(Item mail,Item deadBird)
    {
        this.mail = mail;
        this.deadBird = deadBird;
        Debug.Log(this.mail == null);
    }

    public override void Completed()
    {
        if (!completed)
        {
            completed = true;
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            DialogueHandler.Instance.Chat(player, "Did you say a bird?");
            deadBird.gameObject.SetActive(true);
        }
    }

    public override void Started()
    {
        // throw new System.NotImplementedException();
    }

    public override bool Validate<T>(T extraInfo)
    {


        if (extraInfo.gameObject.TryGetComponent<Player>(out Player player))
        {
            Item item = player.ItemHolder.GetItem();
            if (item && item.Data.name == mail.Data.name)
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
        return false;
    }
}
