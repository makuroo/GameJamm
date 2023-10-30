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


}
