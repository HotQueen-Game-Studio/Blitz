using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIMovimentation))]
public class NPC : Character
{
    public AIMovimentation movimentation { private set; get; }
    public StateMachine<NPC> stateMachine { get; protected set; }

    protected new void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<NPC>(this);
    }

    protected void Start()
    {
        movimentation = this.GetComponent<AIMovimentation>();
    }

    protected void Update()
    {
        stateMachine.Update();
    }

    public override void Attack()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position, attackRange, this.transform.forward);
        foreach (RaycastHit2D hit in hits)
        {
            Rigidbody2D rb2d = hit.collider.attachedRigidbody;
            if (rb2d && rb2d.TryGetComponent<Character>(out Character character) && this != character)
            {
                character.Attributes.Health -= this.Attributes.Damage;
                Instantiate(character.HurtParticleEffect, hit.point, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
