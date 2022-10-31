using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Structure
{
    private Machine_Puzzle machine_Puzzle;
    [SerializeField] private Item key;
    [SerializeField] private GameObject reward;

    private void Awake()
    {
        machine_Puzzle = new Machine_Puzzle(key,reward);
    }

    public override void Interact(Character character)
    {
        machine_Puzzle.Validate(character);
    }

    protected override void Interact()
    {

    }
}
