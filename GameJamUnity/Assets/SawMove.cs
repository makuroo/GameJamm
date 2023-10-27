using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMove : MonoBehaviour
{
    [SerializeField] Vector3 originalPos;
    [SerializeField] Vector3 targetPos;
    [SerializeField] private float speed;
    [SerializeField] private float interval;
    private Vector3 currTarget;
    private bool moving = true;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.localPosition;
        currTarget = targetPos;
        StartCoroutine(MoveSaw());
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, currTarget, speed * Time.deltaTime);
    }

    private IEnumerator MoveSaw()
    {
        while (moving)
        {
            yield return new WaitForSeconds(interval);
            if (transform.localPosition == targetPos)
            {
                currTarget = originalPos;
            }
            else if (transform.localPosition == originalPos)
            {
                currTarget = targetPos;
            }
        }
    }
}
