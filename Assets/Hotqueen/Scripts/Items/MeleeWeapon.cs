using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float radius;
    [SerializeField] private AudioClip hitSound;

    public override void Equip(Character character)
    {
        base.Equip(character);
    }

    public override void Use()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(owner.Aim.position, radius, owner.Aim.right, range, attackLayers);
        foreach (RaycastHit2D hit in hits)
        {
            //play Sound
            itemAudioSource.clip = hitSound;
            itemAudioSource.Play();

            //hit target
            Rigidbody2D rb2d = hit.collider.attachedRigidbody;
            if (rb2d && rb2d.gameObject.GetInstanceID() != owner.gameObject.GetInstanceID())
            {
                // Debug.Log(owner.name + " attacked " + rb2d.name + " with a " + Data.name);
                // Debug.Log("Atacking");

                //Screen shake if owner is player
                if (owner is Player)
                {
                    CameraSettings cameraSettings = FindObjectOfType<CameraSettings>();
                    cameraSettings.ShakeCamera(5, 0.5f);
                }

                if (rb2d.TryGetComponent<Character>(out Character character))
                {
                    character.Attributes.Health -= damage;
                    Instantiate(character.HurtParticleEffect, hit.point, Quaternion.identity);
                }
                else if (rb2d.TryGetComponent<Structure>(out Structure structure))
                {
                    structure.Resistance -= damage;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (owner)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(owner.Aim.position + (owner.Aim.right * range), radius);
        }
    }
}
