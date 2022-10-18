using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData data;
    [SerializeField] protected AudioClip pickClip;
    [SerializeField] protected AudioSource itemAudioSource;

    public Character owner { get; private set; }
    public ItemData Data { get { return data; } }

    public virtual void Pick(Character character)
    {
        if (character != null)
        {
            if (character is Player)
            {
                character.GetComponent<Player>().Inventory.AddItemInAvailableSlot(Data);
            }
            Destroy(this.gameObject);
        }
    }
    public virtual void Drop()
    {
        Debug.Log("Dropping Item");
    }
    public virtual void Use()
    {
        Debug.Log("Using Item");
    }

    public virtual void Equip(Character character)
    {
        itemAudioSource.clip = pickClip;
        itemAudioSource.Play();
        owner = character;
    }

    public virtual void UnequipItem()
    {
        Debug.Log("Unequiping Item");
        owner = null;
    }
}
