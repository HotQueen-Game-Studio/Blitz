using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public BlitzInputs blitzInputs { private set; get; }
    [SerializeField] private SimpleInventory inventory;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private ItemHolder itemHolder;
    public ItemHolder ItemHolder { get { return itemHolder; } }

    public SimpleInventory Inventory { get { return inventory; } }
    private Vector2 aimDirection;

    private void Awake()
    {
        base.Awake();
        blitzInputs = new BlitzInputs();
        blitzInputs.Player.Fire.performed += ctx => Attack();
        blitzInputs.Player.Interact.performed += ctx => Interact();
        blitzInputs.Player.Drop.performed += ctx => DropItem();
        // OnDeath += () =>
        // {
        //     SceneManager.LoadScene("Menu");
        // };
    }

    private void Update()
    {
        RotateAim();
    }

    private void RotateAim()
    {
        //update indicator
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = (mousePos - (Vector2)this.transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        Aim.transform.localPosition = aimDirection;
        Aim.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void OnEnable()
    {
        blitzInputs.Enable();
    }

    private void OnDisable()
    {
        blitzInputs.Disable();
    }

    public override void Attack()
    {
        if (inventory && inventory.GetCurrentSlot().data != null)
        {
            Debug.Log("Using Item:" + inventory.GetCurrentSlot().data.name);
            itemHolder.animator.Play("MeleeAttack");
        }
    }

    public override void Interact()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)this.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, interactRange, interactLayers);
        if (hit && hit.collider.attachedRigidbody)
        {
            Rigidbody2D rb = hit.collider.attachedRigidbody;
            if (rb.TryGetComponent<Item>(out Item item))
            {
                item.Pick(this);
            }
            if (rb.TryGetComponent<Character>(out Character character))
            {
                character.Interacted();
            }
            // if (rb.TryGetComponent<ExitDoor>(out ExitDoor door))
            // {
            //     if (itemHolder.GetItem() != null && itemHolder.GetItem().Data.name == "Key")
            //     {
            //         door.Open();
            //     }
            //     else
            //     {
            //         DialogueHandler.Instance.Chat(this, "I don´t have the key yet.");
            //     }
            // }
        }
    }
    private void DropItem()
    {
        InventorySlot slot = inventory.GetCurrentSlot();
        if (slot && slot.data != null)
        {
            GameObject pref = Resources.Load<GameObject>("Prefabs/Items/" + slot.data.name);
            Item item = Instantiate(pref, this.transform.position, Quaternion.identity).GetComponent<Item>();
            item.Drop();
            inventory.RemoveItem();
        }
    }

    public override void Interacted()
    {
        Debug.Log("Being Interacted");
    }

    private void OnDrawGizmos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)this.transform.position).normalized;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, direction * interactRange);
    }
}
