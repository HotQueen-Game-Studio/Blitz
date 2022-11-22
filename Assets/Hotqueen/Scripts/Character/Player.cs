using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    public BlitzInputs blitzInputs { private set; get; }
    [SerializeField] private SimpleInventory inventory;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator aimAnimator;
    [SerializeField] private ItemHolder itemHolder;

    private Rigidbody2D rb;
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

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RotateAim();
        animator.SetFloat("rbVelMag", rb.velocity.SqrMagnitude());
    }

    private void RotateAim()
    {
        //update indicator
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = (mousePos - (Vector2)this.transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        Aim.transform.localPosition = aimDirection * 1;
        Aim.transform.eulerAngles = new Vector3(0, 0, angle);
        
        if (Aim.transform.rotation.z > 0.5f || Aim.transform.rotation.z < -0.5f)
        {
            
            itemHolder.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {   
            itemHolder.transform.localScale = new Vector3(1, 1, 1);
        }
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
        if (inventory.gameObject.activeInHierarchy && inventory.enabled && inventory.GetCurrentSlot().data != null)
        {
            Debug.Log("Using Item:" + inventory.GetCurrentSlot().data.name);
            // itemHolder.animator.Play("MeleeAttack");
            itemHolder.GetItem().Use();
        }
        AnimateAim();
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
            if (rb.TryGetComponent<Structure>(out Structure structure))
            {
                structure.Interact(this);
            }
        }
        AnimateAim();
    }
    private void DropItem()
    {
        InventorySlot slot = inventory.GetCurrentSlot();
        if (slot && slot.data != null)
        {
            GameObject pref = Resources.Load<GameObject>("Prefabs/Items/" + slot.data.name);
            Item item = Instantiate(pref, itemHolder.transform.position, Quaternion.identity).GetComponent<Item>();
            item.Drop();
            inventory.RemoveItem();
        }
    }

    public override void Interacted()
    {
        Debug.Log("Being Interacted");
    }

    private void AnimateAim()
    {
        aimAnimator.Play("AimClick");
    }
    public void EnableInventory()
    {
        Inventory.gameObject.SetActive(true);
    }
    public void DisableInventory()
    {
        Inventory.gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)this.transform.position).normalized;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, direction * interactRange);
    }

}
