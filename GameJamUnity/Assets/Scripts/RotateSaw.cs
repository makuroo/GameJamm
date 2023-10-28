using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSaw : MonoBehaviour
{
    [SerializeField] private Transform rotateTarget;
    [SerializeField] private float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        transform.localEulerAngles += new Vector3(0, 0, 1) * rotateSpeed * Time.deltaTime;
    }
}
