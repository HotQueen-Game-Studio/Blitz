using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Quest_DamageTarget : Quest
{
    [SerializeField] private GameObject target;
    private int fullHealth;
    [SerializeField] private int damageAmount = 1;

    public override void StartQuest()
    {
        base.StartQuest();
        Player player = GameObject.FindObjectOfType<Player>();
        player.blitzInputs.Player.Fire.performed += Validate;
        if (target.TryGetComponent<Structure>(out Structure structure))
        {
            fullHealth = (int)structure.Resistance;
        }
        else if (target.TryGetComponent<Character>(out Character character))
        {
            fullHealth = character.Attributes.Health;
        }
    }

    private void Validate(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            if (target.TryGetComponent<Structure>(out Structure structure) && fullHealth - (int)structure.Resistance >= damageAmount)
            {
                CompleteQuest();
            }
            else if (target.TryGetComponent<Character>(out Character character) && fullHealth - (int)character.Attributes.Health >= damageAmount)
            {
                CompleteQuest();
            }
        }
    }

    public override void CompleteQuest()
    {
        base.CompleteQuest();
    }
}
