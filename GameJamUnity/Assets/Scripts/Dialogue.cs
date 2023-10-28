using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public float typingSpeed = 0.1f; // Adjust the speed of typing

    private TMP_Text textComponent;
    private string fullText;
    private bool isTyping = false;
    private int characterIndex;
    private bool gembok = false; //nantidiapus begitu buttondiapus

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        fullText = textComponent.text;
        textComponent.text = "";


    }

    public void StartText()
    {
        if (!gembok)
        {
            StartCoroutine(TypeText());
            gembok = true;
        }

    }

    private void Update()
    {
        if (isTyping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (characterIndex < fullText.Length)
                {
                    StopAllCoroutines();
                    textComponent.text = fullText;
                    StartCoroutine(TypingFalseDelay());
                }

            }
        }

        if (!isTyping)
        {
            if (Input.GetKeyDown(KeyCode.Space) && textComponent.text == fullText)
            {
                Debug.Log("NonaktifkanText");
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator TypingFalseDelay()
    {
        yield return new WaitForSeconds(0.3f);
        isTyping = false;
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        characterIndex = 0;

        while (characterIndex < fullText.Length)
        {
            textComponent.text += fullText[characterIndex];
            characterIndex++;

            yield return new WaitForSeconds(typingSpeed);

        }

        isTyping = false;
    }
}
