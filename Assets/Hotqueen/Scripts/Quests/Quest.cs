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

    public virtual void StartQuest()
    {
        OnStarted?.Invoke();
    }
    public abstract void ValidateQuest();
    public abstract void CompleteQuest();
}