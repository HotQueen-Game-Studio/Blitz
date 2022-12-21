using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleDoor : MonoBehaviour
{
    [SerializeField] private bool locked = false;
    public bool Locked
    {
        get
        {
            return locked;
        }
        set
        {
            locked = value;
            // Debug.Log("locke " + locked);
            if (this.gameObject.TryGetComponent<NavMeshObstacle>(out NavMeshObstacle navMeshObstacle))
            {
                navMeshObstacle.enabled = !locked;
            }
        }
    }
    [SerializeField] private SpriteRenderer doorClosed;
    [SerializeField] private SpriteRenderer doorOpened;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Collider2DValidation.IsCharacter(other.collider))
            OpenDoor();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Collider2DValidation.IsCharacter(other))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (Collider2DValidation.IsCharacter(other))
        {
            CloseDoor();
        }
    }

    public void CloseDoor()
    {
        // Debug.Log("Closing door");
        doorOpened.gameObject.SetActive(false);
        doorClosed.gameObject.SetActive(true);
    }
    public void OpenDoor()
    {
        if (locked == false)
        {
            doorOpened.gameObject.SetActive(true);
            doorClosed.gameObject.SetActive(false);
        }
    }

}
