using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    private List<Quest> questList = new List<Quest>();
    public List<Quest> QuestList { get { return questList; } }

    private List<Quest> activeQuestList = new List<Quest>();
    public List<Quest> ActiveQuestList { get { return activeQuestList; } }

    [SerializeField] private Transform questParent = null;

    private void Start()
    {
        InitQuestList();
    }

    public void BeginQuest(string name)
    {
        foreach (Quest quest in questList)
        {
            if (quest.name == name && !activeQuestList.Contains(quest))
            {
                activeQuestList.Add(quest);
                quest.gameObject.SetActive(true);
                quest.StartQuest();
            }
        }
    }
    public void BeginQuest(Quest quest)
    {
        BeginQuest(quest.name);
    }

    public void InitQuestList()
    {
        foreach (Transform child in questParent)
        {
            if (child.TryGetComponent<Quest>(out Quest quest))
            {
                questList.Add(quest);

                if (quest.ActiveOnAwake)
                {
                    BeginQuest(quest.name);
                }
            }
        }
    }
}
