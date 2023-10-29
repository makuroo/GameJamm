using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private ItemStatus player;
    [SerializeField] private float timeBeforeSpawn;
    [SerializeField] private float maxXSpawnCoord;
    [SerializeField] private float maxYSpawnCoord;
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
        Vector2 randomSpawnPos = new Vector2(Random.Range(-maxXSpawnCoord, maxYSpawnCoord + 1), Random.Range(-maxYSpawnCoord, maxYSpawnCoord + 1));

        Debug.Log(randomSpawnPos);
        spawnedItem =  Instantiate(item, randomSpawnPos, Quaternion.identity);
    }

}
