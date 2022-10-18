using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovimentation : MonoBehaviour
{
    public NavMeshAgent agent { private set; get; }
    [SerializeField] private float destinationDistance = 0.2f;
    public Action OnPosReached;
    private Action OnUpdate;

    void Start()
    {
        // agent = this.gameObject.GetComponent<NavMeshAgent>();
        // agent.enabled = true;
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    public void MoveToPosition(Vector2 pos, Action OnReach = null)
    {
        agent.destination = pos;
        agent.isStopped = false;
        OnPosReached = OnReach;

        OnUpdate += () =>
        {
            if (IsDestinationReached())
            {
                OnPosReached?.Invoke();
                OnUpdate = null;
                agent.isStopped = true;
            }
        };
    }

    public bool IsDestinationReached()
    {
        if ((agent.destination - this.transform.position).sqrMagnitude <= destinationDistance)
        {
            return true;
        }
        return false;
    }
}
