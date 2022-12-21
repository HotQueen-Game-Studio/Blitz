using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : State<Silhouette>
{
    private static Killer instance;
    public static Killer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Killer();
            }
            return instance;
        }
    }

    private Killer() { }
    public void Enter(Silhouette character)
    {
        character.SwitchToRedSilhuette();
        character.movimentation.agent.speed = 10;
    }

    public void Execute(Silhouette character)
    {
        Player player = GameObject.FindObjectOfType<Player>();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (player.transform.position - character.transform.position).normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(character.transform.position, direction, character.AttackRange, character.AttackLayers);

        character.movimentation.MoveToPosition(player.transform.position);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.attachedRigidbody && hit.collider.attachedRigidbody.tag == "Player")
            {
                GameManager.Instance.DisableCurrentRoom();
                GameManager.Instance.GetCameraSettings().FollowAndTargetCredits();
                ScreenRedirection.GoToScreen(GameManager.Instance.GetCreditsUI());
                GameObject.Destroy(character.gameObject);
            }
        }
    }

    public void Exit(Silhouette character)
    {

    }

    public bool OnMessage(Silhouette character, Message msg)
    {
        return false;
    }
}
