using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string name;
    public string description;
    public Sprite spt;

    public ItemData() { }

    public ItemData(string name, string description, Sprite spt)
    {
        this.name = name;
        this.description = description;
        this.spt = spt;
    }


}