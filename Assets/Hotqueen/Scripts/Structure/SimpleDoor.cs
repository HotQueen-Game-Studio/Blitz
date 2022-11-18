using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    public bool locked = false;
    [SerializeField] private SpriteRenderer doorClosed;
    [SerializeField] private SpriteRenderer doorOpened;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!locked)
            OpenDoor();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OpenDoor();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CloseDoor();
    }

    public void CloseDoor()
    {
        doorOpened.gameObject.SetActive(false);
        doorClosed.gameObject.SetActive(true);
    }
    public void OpenDoor()
    {
        doorOpened.gameObject.SetActive(true);
        doorClosed.gameObject.SetActive(false);
    }
}
