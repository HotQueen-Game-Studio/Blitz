using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silhouette : NPC
{
    [SerializeField] private Sprite RedSilhouette;
    [SerializeField] private SpriteRenderer GFX;
    public new StateMachine<Silhouette> stateMachine { get; private set; }
    [SerializeField] private Animator animator;
    [SerializeField] private Mail_Puzzle mail_Puzzle;
    public bool twisted;
    [SerializeField] private Item mail;
    [SerializeField] private Item deadBirdInWorld;
    private new void Awake()
    {
        // base.Awake();
        stateMachine = new StateMachine<Silhouette>(this, Idle.Instance, SilhouetteGlobalState.Instance);
        Attributes.HealthReduced += TwistSilhouette;
        mail_Puzzle = new Mail_Puzzle(mail,deadBirdInWorld);
    }

    private void TwistSilhouette(int reduced, int normal)
    {
        if (reduced <= 0)
        {
            twisted = true;
        }
    }

    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        animator.SetFloat("rbVelMag", movimentation.agent.velocity.magnitude);
        stateMachine.Update();
    }
    public void SwitchToRedSilhuette()
    {
        GFX.sprite = RedSilhouette;
        GFX.color = Color.red;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (twisted && other.collider.attachedRigidbody && other.collider.attachedRigidbody.GetComponent<Player>())
        {
            GameManager.Instance.GetCameraSettings().FollowAndTargetCredits();
            GameManager.Instance.GetScreenRedirection().GoToScreen(GameManager.Instance.GetCreditsUI());
            Destroy(this.gameObject);
        }
    }

    public override void Interacted()
    {
        mail_Puzzle.Validate<Player>(GameObject.FindObjectOfType<Player>());
    }
}
