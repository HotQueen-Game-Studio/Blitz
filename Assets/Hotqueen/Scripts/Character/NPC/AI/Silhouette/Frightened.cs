using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frightened : State<Silhouette>
{
    public void Enter(Silhouette character)
    {
        
    }

    public void Execute(Silhouette character)
    {

    }

    public void Exit(Silhouette character)
    {

    }

    public bool OnMessage(Silhouette character, Message msg)
    {
        return false;
    }
}
