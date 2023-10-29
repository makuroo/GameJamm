using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbilBolaPuzzle : MonoBehaviour
{

    public GameObject tempatBolaKosong;
    public GameObject tempatBolaAda;
    public GameObject bolaAtasPlayer;
    private bool gembok = true;
    public GameObject space;
    public PuzzleController puzzleControllerScript;
    [SerializeField]
    private int hasBallBall;

    private void Start() //NANTI DIAPUS WAJIB
    {
        PlayerPrefs.SetInt("HasBall", 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gembok && puzzleControllerScript.isWin == true)
        {
            Debug.Log("AmbilBola");
            tempatBolaAda.SetActive(false);
            tempatBolaKosong.SetActive(true);
            bolaAtasPlayer.SetActive(true);
            space.SetActive(false);
            PlayerPrefs.SetInt("HasBall", 1);

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasBallBall = PlayerPrefs.GetInt("HasBall");

        if (collision.CompareTag("Player") && puzzleControllerScript.isWin == true && hasBallBall == 0)
        {
            Debug.Log("masuk collider pick up ball");
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
