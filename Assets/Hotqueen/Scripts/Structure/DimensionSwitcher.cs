using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitcher : Structure
{
    [SerializeField] private SpriteRenderer GFX;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Item deadBird;
    [SerializeField] private ScreenRedirection screenRedirection;
    [SerializeField] private GameObject creditsUI;
    public override float Resistance { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.GetComponent<Player>())
            GFX.sprite = pressedSprite;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<Player>(out Player player))
        {
            if (player.ItemHolder.GetItem() && player.ItemHolder.GetItem().Data.name == deadBird.name)
            {
                // screenRedirection.GoToScreen(GameManager.Instance.GetCreditsUI());
                GameManager.Instance.ReloadGame();
                return;
            }
            GFX.sprite = defaultSprite;
            GameManager.Instance.SwitchRoom(1);
        }
    }
}
