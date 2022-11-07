using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentationExtraEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem SmokeWhileWalking;

    public void InstantiateSmokeWhileWalking()
    {
        Vector3 spawnPos = this.transform.Find("WalkEffectPoint") != null ? this.transform.Find("WalkEffectPoint").position : this.transform.position;
        ParticleSystem particleSystem = Instantiate<ParticleSystem>(SmokeWhileWalking, spawnPos, Quaternion.identity);
        particleSystem.Play();
    }
}
