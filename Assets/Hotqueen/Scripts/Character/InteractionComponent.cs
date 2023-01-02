using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{

    public delegate void InteractionDelegate(GameObject character, GameObject other);
    public InteractionDelegate OnInteract;
    public InteractionDelegate OnInteracted;

    [SerializeField] protected float interactRange;
    [SerializeField] protected LayerMask interactLayers;

    public virtual void Interact()
    {
        GameObject other = null;
        OnInteract?.Invoke(this.gameObject, other);
    }

    public virtual void Interacted(GameObject other)
    {
        OnInteracted?.Invoke(this.gameObject, other);
    }
}
