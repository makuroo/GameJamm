using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatus : MonoBehaviour
{
    public GameObject item;
    [SerializeField] private PhaseController boss;
    private void Update()
    {
        if (item != null)
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            boss.hitCount++;
            Destroy(item);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Item"))
        {
            item = collision.gameObject;
            item.transform.SetParent(transform);
            item.transform.localPosition = new Vector2(0, 1f);
        }
    }

}