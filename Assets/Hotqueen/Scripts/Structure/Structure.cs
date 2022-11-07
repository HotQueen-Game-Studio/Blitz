using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public abstract float Resistance { get; set; }
    public abstract void Interact(Character character);
    protected abstract void Interact();
    public Action OnDamaged;
}
