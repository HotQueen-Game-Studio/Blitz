using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WorldButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = this.transform.GetComponent<Button>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Collider2DValidation.IsPlayer(other))
        {
            button.onClick?.Invoke();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
