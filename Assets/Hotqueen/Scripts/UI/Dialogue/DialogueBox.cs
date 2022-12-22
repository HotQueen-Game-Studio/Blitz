using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

public class DialogueBox : MonoBehaviour
{
    float defTime = 2;
    float timeLeft;

    [SerializeField] private TMP_Text text;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clip;

    private string[] messages = new string[1];
    private int messageIndex = 0;
    public Action OnCompletedDialogue;

    public void SetText(string msg)
    {
        char[] chars = msg.ToCharArray();
        String[] words = msg.Split(" ");
        timeLeft = defTime + (words.Length / 2);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        SpellDialogue(chars);
    }

    public void SetText(string[] msg)
    {
        messageIndex = 0;
        messages = msg;
        SetText(messages[0]);
    }

    private void Update()
    {

        if (timeLeft > 0) //it fades the text when timeleft is near to 0
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 1f) //finish
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
                        OnCompletedDialogue?.Invoke();
                        GameObject.Destroy(this.gameObject);
                    }

                }
            }
        }
    }

    private async void SpellDialogue(char[] chars)
    {
        text.text = "";
        //add letter by letter
        foreach (char c in chars)
        {
            text.text += c;
            audioSource.clip = clip[Random.Range(0, clip.Length)];
            audioSource.Play();
            await Task.Delay(50);
        }
    }
}
