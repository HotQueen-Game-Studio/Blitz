using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueBox : MonoBehaviour
{
    float defTime = 2;
    float timeLeft;

    [SerializeField] private TMP_Text text;

    private string[] messages = new string[1];
    private int messageIndex = 0;

    private Action OnUpdate;

    public void SetText(string msg)
    {
        text.text = msg;

        string[] words = msg.Split(" ");
        timeLeft = defTime + (words.Length / 2);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

        OnUpdate = () =>
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 1f)
            {
                float alpha = Mathf.Lerp(0, 1, timeLeft);
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

                if (timeLeft <= 0)
                {
                    if (messageIndex < messages.Length - 1)
                    {
                        messageIndex++;
                        SetText(messages[messageIndex]);
                    }
                    else
                    {
                        OnUpdate = null;
                        GameObject.Destroy(this.gameObject);
                    }
                }

            }
        };
        // Debug.Log("Message:" + msg + " Time:" + timeLeft);
    }

    public void SetText(string[] msg)
    {
        messageIndex = 0;
        messages = msg;
        SetText(messages[0]);
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }
}
