using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptNPC : MonoBehaviour
{
    public Dialogue dialogueScript;
    private bool gembok;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !gembok)
        {
            dialogueScript.StartText();
            Invoke("ResetText", 15f);
            gembok = true;
        }
    }


}
