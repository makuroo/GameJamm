using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTransition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Chakram")|| collision.CompareTag("Sawblade"))
        {

        }


    }

    private void StartTransition()
    {

    }
}
