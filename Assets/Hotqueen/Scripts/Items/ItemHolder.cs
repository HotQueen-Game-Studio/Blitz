using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ItemHolder : MonoBehaviour
{
    public Animator animator { private set; get; }

    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void UseItem()
    {
        Item item = GetItem();
        if (item)
        {
            item.Use();
        }
    }

    public void Clear()
    {
        foreach (Transform t in this.transform)
        {
            if (t.gameObject.TryGetComponent<Item>(out Item item))
            {
                item.UnequipItem();
            }

            Destroy(t.gameObject);
        }
    }
    public Item GetItem()
    {
        return this.gameObject.GetComponentInChildren<Item>();
    }
}
