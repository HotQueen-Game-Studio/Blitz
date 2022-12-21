using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<NonPlayableCharacter> where NonPlayableCharacter : NPC
{

    private NonPlayableCharacter myOwner;
    public State<NonPlayableCharacter> previousState { private set; get; }
    public State<NonPlayableCharacter> currentState { private set; get; }
    public State<NonPlayableCharacter> globalState { private set; get; }

    public bool handleMessages(Message msg)
    {
        if (currentState.OnMessage(myOwner, msg))
        {
            return true;
        }

        if (globalState != null && globalState.OnMessage(myOwner, msg))
        {
            return true;
        }

        return false;
    }

    public StateMachine(NonPlayableCharacter myOwner, State<NonPlayableCharacter> currenState = null, State<NonPlayableCharacter> globalState = null)
    {
        this.myOwner = myOwner;
        this.previousState = null;
        this.currentState = currenState;
        this.globalState = globalState;
    }

    public void ChangeState(State<NonPlayableCharacter> state)
    {
        if (currentState != null)
        {
            currentState.Exit(myOwner);
            previousState = currentState;
        }

        currentState = state;
        currentState.Enter(myOwner);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Execute(myOwner);
        }

        if (globalState != null)
        {
            globalState.Execute(myOwner);
        }
    }

    public void PreviousState()
    {
        ChangeState(previousState);
    }
}
