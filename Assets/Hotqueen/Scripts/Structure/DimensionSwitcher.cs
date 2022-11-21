using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitcher : Structure
{
    public override float Resistance { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void Interact(Character character)
    {
        GameManager.Instance.SwitchRoom(1);
    }

    protected override void Interact()
    {
        throw new System.NotImplementedException();
    }
}
