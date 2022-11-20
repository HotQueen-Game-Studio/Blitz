using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsItem(other, out Item item))
        {
            item.transform.parent = this.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (IsItem(other, out Item item))
        {
            item.transform.parent = null;
        }
    }

    public bool IsItem(Collider2D col)
    {
        return IsItem(col, out Item item);
    }
    public bool IsItem(Collider2D col, out Item item)
    {
        item = null;
        return col.attachedRigidbody && col.attachedRigidbody.TryGetComponent<Item>(out item);
    }
}
