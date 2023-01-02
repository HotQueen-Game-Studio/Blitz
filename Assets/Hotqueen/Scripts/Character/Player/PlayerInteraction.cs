using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : InteractionComponent
{
    [SerializeField] private Player player = null;



    private Vector2 aimDirection;


    private void Start()
    {
        player.blitzInputs.Player.Interact.performed += ctx => Interact();
    }

    private void Update()
    {
        RotateAim();
    }


    public override void Interact()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)this.transform.position).normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, direction, interactRange, interactLayers);

        foreach (RaycastHit2D hit in hits)
        {

            if (hit && hit.collider.attachedRigidbody && hit.collider.attachedRigidbody != this)
            {
                Rigidbody2D rb = hit.collider.attachedRigidbody;
                if (rb.TryGetComponent<Item>(out Item item))
                {
                    item.Pick(player);
                }
                if (rb.TryGetComponent<InteractionComponent>(out InteractionComponent interaction))
                {
                    interaction.Interacted(player.gameObject);
                    OnInteract?.Invoke(this.gameObject, interaction.gameObject);
                }
            }
        }
        player.AnimateAim();
    }

    private void RotateAim()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = (mousePos - (Vector2)this.transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        float range = Vector2.Distance((Vector2)this.transform.position, mousePos);
        range = Mathf.Clamp(range, 0, interactRange);

        player.Aim.transform.localPosition = aimDirection * range;

        // if (Vector2.Distance((Vector2)this.transform.position, mousePos) > range)
        // {
        //     Mouse.current.WarpCursorPosition(Camera.main.WorldToScreenPoint(this.transform.position Aim.transform.position));
        // }

        player.Aim.transform.eulerAngles = new Vector3(0, 0, angle);

        // this fix the item sprite rendered inverted texture
        if (player.Aim.transform.rotation.z > 0.5f || player.Aim.transform.rotation.z < -0.5f)
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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)this.transform.position).normalized;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, direction * interactRange);
    }
}
