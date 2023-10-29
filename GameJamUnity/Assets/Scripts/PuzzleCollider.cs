using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCollider : MonoBehaviour
{

    public PuzzleController puzzleControllerScript;
    public GameObject space;
    public bool gembok = true;
    public PlayerMovement playerMovementScript;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gembok)
        {
            Debug.Log("Space Pressed while treadmill");
            puzzleControllerScript.StartThePuzzle();
            gameObject.SetActive(false);
            space.SetActive(false);
            playerMovementScript.isRolling = true;
            playerMovementScript.movementSpeed = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            space.SetActive(true);
            gembok = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            space.SetActive(false);
            gembok = true;
        }
    }



}
