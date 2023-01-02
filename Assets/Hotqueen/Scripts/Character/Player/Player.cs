using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    public BlitzInputs blitzInputs { private set; get; }
    [SerializeField] private SimpleInventory inventory;
    [SerializeField] private Animator animator;


    [SerializeField] private ItemHolder itemHolder;
    public ItemHolder ItemHolder { get { return itemHolder; } }

    private Rigidbody2D rb;

    public SimpleInventory Inventory { get { return inventory; } }

    private void Awake()
    {
        base.Awake();
        blitzInputs = new BlitzInputs();
        blitzInputs.Player.Fire.performed += ctx => Attack();
        blitzInputs.Player.Drop.performed += ctx => DropItem();
        // OnDeath += () =>
        // {
        //     SceneManager.LoadScene("Menu");
        // };
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        DisableCursor();
    }

    private void DisableCursor()
    {
        Cursor.visible = false;
    }
    private void EnableCursor()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        animator.SetFloat("rbVelMag", rb.velocity.SqrMagnitude());
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
            // itemHolder.animator.Play("Use");
            itemHolder.GetItem().Use();
        }
    }

    public void DropItem()
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

    public void EnableInventory()
    {
        Inventory.gameObject.SetActive(true);
    }
    public void DisableInventory()
    {
        Inventory.gameObject.SetActive(false);
    }


}
