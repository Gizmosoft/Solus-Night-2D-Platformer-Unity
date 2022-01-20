using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    private void Start()
    {
        StartCoroutine(Type());
    }

    // Coroutine for delaying the text appearance (for the typing effect)
    IEnumerator Type()
    {
        // Loop running for the amount of time there are characters in our sentence
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            // code after the below line would be deayed by the specified amount of time
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(2.5f);
        NextSentence();
    }

    public void NextSentence()
    {
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
        }
    }
}
