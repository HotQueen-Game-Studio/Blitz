using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dummy : NPC
{

    private void Start()
    {
        base.Start();
        Attributes.HealthReduced += AuchDialog;

    }

    private void AuchDialog(int reduced, int normal)
    {
        string[] messages = { "Ouch!", "Stop IT!", "Mother F*****!" };
        string[] say = new String[2];
        say[0] = messages[Random.Range(0, messages.Length)];
        if (Random.Range(0, 3) == 0)
        {
            say[1] = ">:(";
            stateMachine.ChangeState(FollowAndAttackPlayer.Instance);
        }
        else
        {
            say[1] = "Don´t bother me";
        }
        DialogueHandler.Instance.Chat(this, say);
    }

    public override void Attack()
    {
        Debug.Log("Attack");
        base.Attack();
    }

    public override void Interact()
    {

    }

    public override void Interacted()
    {
        string[] messages = { "Hey you.", "How is it going. ", "Creepy room, eh!?" };
        DialogueHandler.Instance.Chat(this, messages[Random.Range(0, messages.Length)]);
    }
}
