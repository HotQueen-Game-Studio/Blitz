using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WorldButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private Sprite normalButton;
    [SerializeField] private Sprite pressedButton;

    private void Start()
    {
        button = this.transform.GetComponent<Button>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        button.image.sprite = pressedButton;
    }
    void OnTriggerExit2D(Collider2D other)
    {

        button.image.sprite = normalButton;
        if (Collider2DValidation.IsPlayer(other))
        {
            button.onClick?.Invoke();
        }
    }
}
