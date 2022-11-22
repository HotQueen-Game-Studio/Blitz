using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State<Silhouette>
{
    private static Idle instance;
    public static Idle Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Idle();
            }
            return instance;
        }
    }

    public void Enter(Silhouette character)
    {
        // throw new System.NotImplementedException();
    }

    public void Execute(Silhouette character)
    {
        // throw new System.NotImplementedException();
        Player player = GameObject.FindObjectOfType<Player>();
        Item item = player.ItemHolder.GetItem();
        if (item is MeleeWeapon)
        {
            character.stateMachine.ChangeState(Frightened.Instance);
        }
    }

    public void Exit(Silhouette character)
    {
        // throw new System.NotImplementedException();
    }

    public bool OnMessage(Silhouette character, Message msg)
    {
        // throw new System.NotImplementedException();
        return false;
    }
}
