using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silhouette : NPC
{
    [SerializeField] private Sprite RedSilhouette;
    [SerializeField] private SpriteRenderer GFX;
    public new StateMachine<Silhouette> stateMachine { get; private set; }
    [SerializeField] private Animator animator;
    public bool twisted;
    private new void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<Silhouette>(this);
    }

    private new void Start()
    {
        base.Start();
        stateMachine.ChangeState(Frightened.Instance);
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
        }
    }
}
