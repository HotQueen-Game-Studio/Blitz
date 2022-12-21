using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WorldButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private Sprite normalButton;
    [SerializeField] private Sprite pressedButton;
    [SerializeField] private UnityEvent OnReleased;


    private void Start()
    {
        button = this.transform.GetComponent<Button>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        button.image.sprite = pressedButton;
        if (Collider2DValidation.IsPlayer(other))
        {
            button.onClick?.Invoke();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        button.image.sprite = normalButton;

        if (Collider2DValidation.IsPlayer(other))
        {
            OnReleased?.Invoke();
        }
    }
}
