using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuanganPuzzle : MonoBehaviour
{

    public PlayerMovement playerMovementScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovementScript.isRolling = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovementScript.isRolling = false;
        }
    }

}
