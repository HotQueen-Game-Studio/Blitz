using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Quest : MonoBehaviour
{
    [SerializeField] protected new string name = null;
    [SerializeField] protected string description = null;

    [SerializeField] protected UnityEvent OnCompleted;
    [SerializeField] protected UnityEvent OnStarted;

    [SerializeField] private bool activeOnAwake = false;
    public bool ActiveOnAwake { get { return activeOnAwake; } }

    public virtual void StartQuest()
    {
        OnStarted?.Invoke();
    }
    public virtual void CompleteQuest()
    {
        OnCompleted?.Invoke();
        Destroy(this.gameObject);
    }
}