using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitcher : Structure
{
    [SerializeField] private SpriteRenderer GFX;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite defaultSprite;
    public override float Resistance { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void Interact(Character character)
    {

    }

    protected override void Interact()
    {
        throw new System.NotImplementedException();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GFX.sprite = pressedSprite;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        GFX.sprite = defaultSprite;
        GameManager.Instance.SwitchRoom(1);
    }
}
