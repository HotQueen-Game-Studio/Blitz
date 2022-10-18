using UnityEngine;
[System.Serializable]
public class CharacterAttributes
{
    public delegate void AttributeChange(int reduced, int normal);
    //REDUCED CALLBACK
    public AttributeChange HealthReduced;
    public AttributeChange StaminaReduced;
    public AttributeChange MindReduced;
    //ADD CALLBACK
    public AttributeChange HealthAdded;
    public AttributeChange StaminaAdded;
    public AttributeChange MindAdded;

    [SerializeField] private int health;
    [SerializeField] private int stamina;
    [SerializeField] private int mind;
    [SerializeField] private int damage;
    public int Health
    {
        set
        {
            if (value < health)
            {
                //reducing
                HealthReduced?.Invoke(value, health);
            }
            else
            {
                //adding
                HealthAdded?.Invoke(value, health);
            }
            health = value;
        }
        get
        {
            return health;
        }
    }
    public int Stamina
    {
        set
        {
            if (value <= 0)
            {
                //reducing
                StaminaReduced?.Invoke(value, stamina);
            }
            else
            {
                //adding
                StaminaAdded?.Invoke(value, stamina);
            }
            stamina = value;
        }
        get
        {
            return stamina;
        }
    }
    public int Mind
    {
        set
        {
            if (value <= 0)
            {
                //reducing
                MindReduced?.Invoke(value, mind);
            }
            else
            {
                //adding
                MindAdded?.Invoke(value, mind);
            }
            mind = value;
        }
        get
        {
            return mind;
        }
    }
    public int Damage
    {
        get
        {
            return damage;
        }
    }

}