using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Collider2D coll;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    public void Die()
    {
        Debug.Log("Mati");
        //transisi mati
        //pop up UI Game Over
    }


}
