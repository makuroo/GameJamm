using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHasTakeBall : MonoBehaviour
{
    bool isInBallHolder = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("HasBall")==1 && (!PlayerPrefs.HasKey("HasPutBall")|| PlayerPrefs.GetInt("HasPutBall") == 0))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }   

        if(isInBallHolder==true && (!PlayerPrefs.HasKey("HasPutBall") || PlayerPrefs.GetInt("HasPutBall") == 0))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerPrefs.SetInt("HasPutBall", 1);
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BallHolder"))
        {
            isInBallHolder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BallHolder"))
        {
            isInBallHolder = false;
        }
    }
}
