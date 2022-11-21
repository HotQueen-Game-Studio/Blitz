using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    [SerializeField] private bool disableOnAwake = true;    
    void Awake()
    {
        if(disableOnAwake){
            GameManager.Instance.AddRoom(this);
            this.transform.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Collider2DValidation.IsItem(other, out Item item))
        {
            item.transform.parent = this.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (Collider2DValidation.IsItem(other, out Item item))
        {
            item.transform.parent = null;
        }
    }
}
