using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : ScriptableObject
{
    public abstract void Started();
    public abstract bool Validate<T>(T extraInfo) where T : MonoBehaviour;
    public abstract void Completed();
}