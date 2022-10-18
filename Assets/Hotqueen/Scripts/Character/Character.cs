using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterAttributes attributes;
    public CharacterAttributes Attributes { get { return attributes; } }

    [SerializeField] private Transform aim;
    public Transform Aim { get { return aim; } }

    protected Action OnDeath;

    [SerializeField] private ParticleSystem hurtParticleEffect;
    public ParticleSystem HurtParticleEffect
    {
        get
        {
            return hurtParticleEffect;
        }
    }
    [SerializeField] protected LayerMask interactLayers;
    [SerializeField] protected float interactRange = 1;
    [SerializeField] protected LayerMask attackLayers;
    [SerializeField] protected float attackRange = 1;


    protected void Awake()
    {
        attributes.HealthReduced += ValidateDeath;
    }

    private void ValidateDeath(int reduced, int normal)
    {
        if (reduced <= 0)
        {
            OnDeath?.Invoke();
            Destroy(this.gameObject);
        }
    }


    public abstract void Interacted();
    public abstract void Attack();
    public abstract void Interact();

}
