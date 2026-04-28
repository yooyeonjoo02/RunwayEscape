using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform[] spawnPoints;

    public float minSpawnTime = 1f;
    public float maxSpawnTime = 2f;

    private GameObject[] spawnedItems;

    private void Start()
    {
        spawnedItems = new GameObject[spawnPoints.Length];
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnRandomItem();
        }
    }

    private void SpawnRandomItem()
    {
        if (itemPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            return;
        }

        int randomIndex = GetRandomEmptySpawnPointIndex();

        if (randomIndex == -1)
        {
            return;
        }

        GameObject randomItem = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject spawnedItem = Instantiate(
            randomItem,
            spawnPoint.position,
            spawnPoint.rotation
        );

        spawnedItems[randomIndex] = spawnedItem;
    }

    private int GetRandomEmptySpawnPointIndex()
    {
        int[] emptyIndexes = new int[spawnPoints.Length];
        int emptyCount = 0;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnedItems[i] == null)
            {
                emptyIndexes[emptyCount] = i;
                emptyCount++;
            }
        }

        if (emptyCount == 0)
        {
            return -1;
        }

        return emptyIndexes[Random.Range(0, emptyCount)];
    }
}