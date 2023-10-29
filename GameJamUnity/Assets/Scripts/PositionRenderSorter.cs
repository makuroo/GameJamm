using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRenderSorter : MonoBehaviour
{
    private Renderer myRenderer;
    [SerializeField]
    private int sortingLayerBase = 5000;
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    private bool runOnlyOnce = false;

    private float timer;
    private float timerMax = .1f;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timerMax;
            myRenderer.sortingOrder = (int)(sortingLayerBase - transform.position.y - offset);
            if (runOnlyOnce)
                Destroy(this);
        }
        
    }
}
