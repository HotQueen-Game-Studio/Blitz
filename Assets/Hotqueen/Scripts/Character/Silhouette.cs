using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silhouette : NPC
{
    private new StateMachine<Silhouette> stateMachine;
    private void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<Silhouette>(this);
    }

    private void Start()
    {
        base.Start();
    }
}
