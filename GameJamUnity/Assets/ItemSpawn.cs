using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private ItemStatus player;
    [SerializeField] private float timeBeforeSpawn;
    [SerializeField] private Transform[] spawnPos;
    private GameObject spawnedItem;
    private float elapsedTime;

    private void Update()
    {
        if (spawnedItem == null){
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeBeforeSpawn)
            {
                elapsedTime = 0;
                SpawnItem();
            }
        }
    }

    private void SpawnItem()
    {
        Vector2 randomSpawnPos = spawnPos[Random.Range(0, spawnPos.Length)].position;

        Debug.Log(randomSpawnPos);
        spawnedItem = Instantiate(item, randomSpawnPos, Quaternion.identity);
    }

}
