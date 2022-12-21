using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
public class Collider2DValidation
{
    public static bool IsPlayer(Collider2D col)
    {
        return IsPlayer(col, out Player player);
    }

    public static bool IsPlayer(Collider2D col, out Player player)
    {
        player = null;
        return col.attachedRigidbody && col.attachedRigidbody.TryGetComponent<Player>(out player);
    }


    public static bool IsCharacter(Collider2D col)
    {
        return IsCharacter(col, out Character character);
    }

    public static bool IsCharacter(Collider2D col, out Character character)
    {
        character = null;
        return col.attachedRigidbody && col.attachedRigidbody.TryGetComponent<Character>(out character);
    }

    public static bool IsItem(Collider2D col)
    {
        return IsItem(col, out Item item);
    }

    public static bool IsItem(Collider2D col, out Item item)
    {
        item = null;
        return col.attachedRigidbody && col.attachedRigidbody.TryGetComponent<Item>(out item);
    }
}