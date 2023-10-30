using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatus : MonoBehaviour
{
    public GameObject item;
    [SerializeField] private PhaseController boss;
    [SerializeField] private GameObject vfx;
    [SerializeField] private Transform tempatLedak;
    public bool holdingBall;
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
            GameObject fx = Instantiate(vfx, tempatLedak);
            boss.hitCount++;
            Destroy(item);
            holdingBall = false;
            Destroy(fx, 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Item"))
        {
            holdingBall = true;
            item = collision.gameObject;
            item.transform.SetParent(transform);
            item.transform.localPosition = new Vector2(0, 1f);
        }
    }

}