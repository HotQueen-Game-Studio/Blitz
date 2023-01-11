using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : InteractionComponent
{
    [SerializeField] private Player player = null;
    [SerializeField] private float itemHolderRange = 1;
    [SerializeField] private Canvas interactionIndicator;
    private Dictionary<GameObject, Canvas> indicatedObjs = new Dictionary<GameObject, Canvas>();
    private Vector2 aimDirection = new Vector2(1, 0);


    private void Start()
    {
        player.blitzInputs.Player.Interact.performed += ctx => Interact();
    }

    private void Update()
    {
        UpdateAim();
        GetHits();
    }

    public void GetHits()
    {
        RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, interactRange, this.transform.up, interactRange, interactLayers);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.attachedRigidbody && hit.collider.attachedRigidbody != this)
            {
                Rigidbody rb = hit.collider.attachedRigidbody;
                if (!indicatedObjs.ContainsKey(rb.gameObject) && rb.TryGetComponent<Item>(out Item item)) //|| rb.TryGetComponent<InteractionComponent>(out InteractionComponent interaction))
                {
                    indicatedObjs.Add(rb.gameObject, Instantiate(interactionIndicator, rb.transform.position + rb.transform.up, Quaternion.identity, rb.transform));
                }
            }
        }

        foreach (GameObject key in indicatedObjs.Keys)
        {
            if (key != null && Vector3.Distance(this.transform.position, key.transform.position) > interactRange)
            {
                Destroy(indicatedObjs[key].gameObject);
                indicatedObjs.Remove(key);
            }
        }
    }

    public override void Interact()
    {
        foreach (GameObject hit in indicatedObjs.Keys)
        {
            if (hit.TryGetComponent<Item>(out Item item))
            {
                item.Pick(player);
            }
            if (hit.TryGetComponent<InteractionComponent>(out InteractionComponent interaction))
            {
                interaction.Interacted(player.gameObject);
                OnInteract?.Invoke(this.gameObject, interaction.gameObject);
            }
        }
    }


    private void UpdateAim()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (rb.velocity.x > 0.1f)
        {
            Debug.Log("Hello");
            player.ItemHolder.transform.localPosition = player.transform.right * (itemHolderRange);
        }
        else if (rb.velocity.x < -0.1f)
        {
            Debug.Log(" Not Hello");
            player.ItemHolder.transform.localPosition = -player.transform.right * (itemHolderRange);
        }



        // this fix the item sprite rendered inverted texture
        if (player.ItemHolder.transform.rotation.z > 0.5f || player.ItemHolder.transform.rotation.z < -0.5f)
        {
            player.ItemHolder.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            player.ItemHolder.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, interactRange);
    }
}
