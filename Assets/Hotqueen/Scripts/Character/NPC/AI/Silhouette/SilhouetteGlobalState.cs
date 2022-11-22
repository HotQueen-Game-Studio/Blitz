using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteGlobalState : State<Silhouette>
{

    private static SilhouetteGlobalState instance;
    public static SilhouetteGlobalState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SilhouetteGlobalState();
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
        if (character.twisted)
        {
            character.stateMachine.ChangeState(Killer.Instance);
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
