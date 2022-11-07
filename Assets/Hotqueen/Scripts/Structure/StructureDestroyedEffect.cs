using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Machine))]
public class StructureDestroyedEffect : MonoBehaviour
{
    private Machine machine;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite structureDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        machine = this.gameObject.GetComponent<Machine>();
        machine.OnDamaged += Damaged;
    }

    private void Damaged()
    {
        if (machine.Resistance <= 0)
        {
            if (particleSystem)
            {
                particleSystem.Play();
            }

            if (spriteRenderer)
            {
                spriteRenderer.sprite = structureDestroyed;
            }
        }
    }
}
