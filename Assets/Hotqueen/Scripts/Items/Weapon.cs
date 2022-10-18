using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    [SerializeField] protected int damage;
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask attackLayers;
}
