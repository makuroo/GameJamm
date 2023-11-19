using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmokeController : MonoBehaviour
{
    public SpriteMask spriteMask; 
    public SpriteRenderer objectYSpriteRenderer; 

    private void Start()
    {
        if (spriteMask == null)
        {
            Debug.LogError("SpriteMask not assigned!");
        }

        if (objectYSpriteRenderer == null)
        {
            Debug.LogError("Object Y SpriteRenderer not assigned!");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        objectYSpriteRenderer.sortingLayerName = "Foreground";
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move Object Y with the mouse drag
        objectYSpriteRenderer.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Set Object Y sorting layer to be below Object X
        objectYSpriteRenderer.sortingLayerName = "Background";
    }


}
