using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallProjectiles : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Vector2 initialDirection;
    [SerializeField] private float projectilesSpeed = 50f;
    [SerializeField] private float returnDelay = 1.5f;
    private bool isReturning = false;
    private Transform bossTransform;

    private void Start()
    {
        bossTransform = GameObject.FindGameObjectWithTag("Boss").transform;
    }

    private void FixedUpdate()
    {
        if (!isReturning)
        {
            rb.velocity = initialDirection * projectilesSpeed;
        }
        else
        {
            Vector2 returnDirection = (bossTransform.position - transform.position).normalized;
            rb.velocity = returnDirection * projectilesSpeed;
        }
    }

    public void SetInitialDirection(Vector2 direction)
    {
        initialDirection = direction.normalized;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DeathTransition>().StartTransition();
        }
        DestroyProjectile();
    }

    public void ReturnToBoss()
    {
        isReturning = true;
        Invoke("DestroyProjectile", returnDelay);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
