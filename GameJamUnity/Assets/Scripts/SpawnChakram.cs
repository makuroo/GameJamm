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
    private bool chakramAtPlayerPosition = false;

    void Start()
    {
        spawnedChakram = null;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f && spawnedChakram == null)
        {
            spawnedChakram = Instantiate(chakramPrefab, player.transform.position, Quaternion.identity);
            chakramAtPlayerPosition = true;

            StartCoroutine(ChakramBehavior());

            cooldownTimer = 6f;
        }
    }

    IEnumerator ChakramBehavior()
    {
        yield return new WaitForSeconds(2f);

        while (Vector3.Distance(spawnedChakram.transform.position, transform.position) > 0.1f)
        {
            Vector3 targetPosition = chakramAtPlayerPosition ? transform.position : player.transform.position;
            Vector3 direction = targetPosition - spawnedChakram.transform.position;
            direction.Normalize();
            Rigidbody2D rb = spawnedChakram.GetComponent<Rigidbody2D>();
            rb.velocity = direction * chakramSpeed;

            yield return null;
        }

        Destroy(spawnedChakram);
    }
}