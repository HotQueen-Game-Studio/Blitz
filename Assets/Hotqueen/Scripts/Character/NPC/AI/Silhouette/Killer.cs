using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : State<Silhouette>
{
    private static Killer instance;
    public static Killer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Killer();
            }
            return instance;
        }
    }

    private Killer() { }
    public void Enter(Silhouette character)
    {
        character.SwitchToRedSilhuette();
    }

    public void Execute(Silhouette character)
    {
        character.movimentation.MoveToPosition(GameObject.FindObjectOfType<Player>().transform.position, () =>
        {
            // character.Attack();
        });
    }

    public void Exit(Silhouette character)
    {

    }

    public bool OnMessage(Silhouette character, Message msg)
    {
        return false;
    }
}
