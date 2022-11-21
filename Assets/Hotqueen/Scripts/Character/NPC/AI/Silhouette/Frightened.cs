using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Frightened : State<Silhouette>
{
    private float walkRadius = 8;
    private float distanceFromPlayer = 8;
    private bool searchNewTarget = true;

    Vector2 target = new Vector2();

    private static Frightened instance;
    public static Frightened Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Frightened();
            }
            return instance;
        }
    }

    private Frightened()
    {

    }

    public void Enter(Silhouette character)
    {
    }

    public void Execute(Silhouette character)
    {

        if (character.twisted)
        {
            character.stateMachine.ChangeState(Killer.Instance);
        }

        if (Vector2.Distance(GameObject.FindObjectOfType<Player>().transform.position, character.transform.position) <= distanceFromPlayer)
        {
            searchNewTarget = true;
        }
        MoveToRandomPositionAwayFromPlayer(character);

    }

    private async void MoveToRandomPositionAwayFromPlayer(Silhouette character)
    {
        if (!searchNewTarget)
        {
            return;
        }

        searchNewTarget = false;

        Vector2 origin = character.transform.position;
        Vector2 newTarget = GetRandomPosition(origin);
        Player player = GameObject.FindObjectOfType<Player>();

        // Debug.Log(Vector2.Distance(target, (Vector2)player.transform.position));

        while (Vector2.Distance((Vector2)player.transform.position, newTarget) <= distanceFromPlayer)
        {
            newTarget = GetRandomPosition(origin);
            // Debug.Log(target);
            await Task.Yield();
        }

        character.movimentation.MoveToPosition(newTarget, () => { searchNewTarget = true; });
    }

    public Vector2 GetRandomPosition(Vector2 origin)
    {
        Vector2 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += origin;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        return hit.position;
    }

    public void Exit(Silhouette character)
    {

    }

    public bool OnMessage(Silhouette character, Message msg)
    {
        // if (msg.Equals("Twisted"))
        // {
        //     character.SwitchToRedSilhuette();
        // }
        return false;
    }
}
