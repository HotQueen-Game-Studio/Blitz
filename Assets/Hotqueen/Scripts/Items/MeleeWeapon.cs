using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float radius;
    [SerializeField] private AudioClip hitSound;
    public override void Use()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(owner.Aim.position, radius, owner.Aim.right, range, attackLayers);
        foreach (RaycastHit2D hit in hits)
        {
            //play Sound
            itemAudioSource.clip = hitSound;
            itemAudioSource.Play();
            //Screen shake if owner is player
            if (owner is Player)
            {
                CinemachineShake cinemachineShake = owner.GetComponentInChildren<CinemachineShake>();
                cinemachineShake.ShakeCamera(5, 1);
            }
            //hit target
            Rigidbody2D rb2d = hit.collider.attachedRigidbody;
            if (rb2d && rb2d.TryGetComponent<Character>(out Character character) && owner != character)
            {
                character.Attributes.Health -= damage;
                Instantiate(character.HurtParticleEffect, hit.point, Quaternion.identity);
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
