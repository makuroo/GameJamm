using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chakram : MonoBehaviour
{
    private Collider2D coll;
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Die();
        }
    }
}
