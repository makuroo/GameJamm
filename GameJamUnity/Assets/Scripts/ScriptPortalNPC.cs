using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptPortalNPC : MonoBehaviour
{

    public Dialogue dialogueScript;
    private bool gembok;
    public GameObject collider2;
    public GameObject collider3;

    public TMP_Text text1;
    public TMP_Text text2;

    private bool gembokk;
    private bool gembokkk;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !gembok)
        {
            dialogueScript.StartText();
            Invoke("ResetText", 15f);
            Invoke("BukaGembok", 2f);
            gembok = true;
        }

    }

    private void Update()
    {
        if(text1.text == "" && gembokk)
        {
            collider2.SetActive(true);
            Invoke("BukaGembokk", 2f);
            gembokk = false;

        }

        if(text2.text == "" && gembokkk)
        {
            collider3.SetActive(true);
            gembokkk = false;

        }

    }

    private void BukaGembokk()
    {
         gembokkk = true;
    }

    private void BukaGembok()
    {
        gembokk = true;
    }

}
