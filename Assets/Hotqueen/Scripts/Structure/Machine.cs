using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Structure
{
    [SerializeField] private SpriteRenderer chipSpriteRenderer;
    [SerializeField] private Silhouette silhouette;

    public override float Resistance { get { return resistance; } set { resistance = value; OnDamaged?.Invoke(); } }
    [SerializeField] private float resistance = 2;

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
