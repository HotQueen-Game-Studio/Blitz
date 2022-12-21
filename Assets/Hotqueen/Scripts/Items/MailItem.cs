using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailItem : Item
{
    [SerializeField] private Canvas itemView;
    public override void Use()
    {
        base.Use();
        itemView.gameObject.SetActive(!itemView.gameObject.activeInHierarchy);
    }
}
