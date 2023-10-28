using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChakram : MonoBehaviour
{
    [SerializeField] private GameObject chakramPrefab;
    [SerializeField] private float chakramSpeed = 2f;
    [SerializeField] private GameObject player;
    private GameObject spawnedChakram;
    private float cooldownTimer = 10f;

    void Start()
    {
        spawnedChakram = null;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f && spawnedChakram == null)
        {
            spawnedChakram = Instantiate(chakramPrefab, transform.position, Quaternion.identity);
            StartCoroutine(ChakramBehavior());
            cooldownTimer = 6f;
        }
    }

    IEnumerator ChakramBehavior()
    {
        yield return new WaitForSeconds(2f);

        Vector3 playerPosition = player.transform.position;
        while (Vector3.Distance(spawnedChakram.transform.position, playerPosition) > 0.1f)
        {
            Vector3 direction = playerPosition - spawnedChakram.transform.position;
            direction.Normalize();
            Rigidbody2D rb = spawnedChakram.GetComponent<Rigidbody2D>();
            rb.velocity = direction * chakramSpeed;

            yield return null;
        }
        Rigidbody2D rbAfterHit = spawnedChakram.GetComponent<Rigidbody2D>();
        rbAfterHit.velocity = Vector2.zero;
        rbAfterHit.angularVelocity = 0f;

        yield return new WaitForSeconds(2f);
        while (Vector3.Distance(spawnedChakram.transform.position, transform.position) > 0.1f)
        {
            Vector3 direction = transform.position - spawnedChakram.transform.position;
            direction.Normalize();
            Rigidbody2D rb = spawnedChakram.GetComponent<Rigidbody2D>();
            rb.velocity = direction * chakramSpeed;

            yield return null;
        }

        Destroy(spawnedChakram);
    }
}