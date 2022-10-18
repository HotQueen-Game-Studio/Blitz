
using UnityEngine;
public class FollowAndAttackPlayer : State<NPC>
{

    private static FollowAndAttackPlayer instance = null;
    public static FollowAndAttackPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FollowAndAttackPlayer();
            }
            return instance;
        }
    }
    public void Enter(NPC character)
    {
    }

    public void Execute(NPC character)
    {
        character.movimentation.MoveToPosition(GameObject.FindObjectOfType<Player>().transform.position, () =>
        {
            character.Attack();
        });

    }

    public void Exit(NPC character)
    {

    }

    public bool OnMessage(NPC character, Message msg)
    {
        throw new System.NotImplementedException();
    }
}
