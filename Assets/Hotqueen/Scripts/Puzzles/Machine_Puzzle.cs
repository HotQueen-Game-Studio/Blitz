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
        GameObject.Instantiate<GameObject>(reward, new Vector3(), Quaternion.identity);
    }

    public override void Started()
    {

    }

    public override bool Validate<T>(T extraInfo)
    {
        if (extraInfo.gameObject.TryGetComponent<Player>(out Player player))
        {
            Item item = player.ItemHolder.GetItem();
            if (item.Data.name == key.Data.name)
            {
                player.Inventory.RemoveItem();
                Debug.Log("Puzzle Completed");
                Completed();
                return true;
            }
        }

        Debug.Log("Sorry maybe it´s the wrong item");
        return false;
    }
}
