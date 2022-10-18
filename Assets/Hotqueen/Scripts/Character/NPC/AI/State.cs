using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State<NonPlayableCharacter> where NonPlayableCharacter : Character
{
    public void Enter(NonPlayableCharacter character);
    public void Execute(NonPlayableCharacter character);
    public void Exit(NonPlayableCharacter character);
    public bool OnMessage(NonPlayableCharacter character,Message msg);
}
