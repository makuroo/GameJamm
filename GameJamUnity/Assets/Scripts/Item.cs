using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroy;
    [SerializeField] private float elapsedTime = 0;

    private void Update()
    {
        

        if (elapsedTime < timeBeforeDestroy)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeBeforeDestroy && transform.parent==null)
            {
                Destroy(gameObject);
            }
        }
    }
}
