using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCollider : MonoBehaviour
{

    public PuzzleController puzzleControllerScript;
    public GameObject space;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            space.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                puzzleControllerScript.StartThePuzzle();
                gameObject.SetActive(false);
                space.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            space.SetActive(false);
        }
    }



}
