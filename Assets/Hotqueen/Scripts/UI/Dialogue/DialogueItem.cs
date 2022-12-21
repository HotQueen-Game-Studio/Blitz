[System.Serializable]
public class DialogueItem
{
    public Character[] characters;
    public string[] messages;

    public DialogueItem(Character[] characters, string[] message)
    {
        this.characters = characters;
        this.messages = message;
    }
}