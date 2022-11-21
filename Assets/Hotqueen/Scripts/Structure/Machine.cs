using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Structure
{
    private Machine_Puzzle machine_Puzzle;
    [SerializeField] private Item key;
    [SerializeField] private SimpleDoor door;
    [SerializeField] private SpriteRenderer chip;
    [SerializeField] private Silhouette silhouette;

    public override float Resistance { get { return resistance; } set { resistance = value; OnDamaged?.Invoke(); } }
    [SerializeField] private float resistance = 2;

    private void Awake()
    {
        machine_Puzzle = new Machine_Puzzle(key, door, silhouette);
        OnDamaged += () =>
        {
            machine_Puzzle.Validate<Machine>(this);
        };
    }

    public override void Interact(Character character)
    {
        if (machine_Puzzle.Validate(character))
        {
            chip.enabled = true;
        }
    }

    protected override void Interact()
    {

    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override string ToString()
    {
        return base.ToString();
    }

    private void OnReceiveDamage()
    {

    }
}
