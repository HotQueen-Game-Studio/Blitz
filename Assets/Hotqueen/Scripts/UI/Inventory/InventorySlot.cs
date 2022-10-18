using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class InventorySlot : MonoBehaviour
{
    public Image slotImage;
    public TMP_Text qntText;
    public Image highlight;
    public ItemData data { private set; get; }
    public int qnt { private set; get; }

    public void SetItem(ItemData data)
    {
        if (this.data == null)
        {
            this.data = data;
            qnt = 1;
        }
        else if (this.data.name == data.name)
        {
            qnt += 1;
        }

        slotImage.sprite = data.spt;
        slotImage.preserveAspect = true;
        qntText.text = qnt.ToString();
    }

    public void ClearItem()
    {
        data = null;
        slotImage.sprite = null;
        slotImage.preserveAspect = true;
        qntText.text = "0";
    }

    public void EnableHighlight()
    {
        highlight.gameObject.SetActive(true);

    }

    public void DisableHighlight()
    {
        highlight.gameObject.SetActive(false);
    }
}
