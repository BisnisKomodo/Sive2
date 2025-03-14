using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NocturnalSpawner : MonoBehaviour
{
    public Transform player; 
    public GameObject enemyPrefab;
    public float spawnRate = 5f; 

    void Start()
    {
        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            SpawnEnemyNearPlayer();
            yield return new WaitForSeconds(spawnRate); 
        }
    }

    void SpawnEnemyNearPlayer()
{
    if (player == null || enemyPrefab == null) return;

    float minDistance = 10f;  
    float maxDistance = 30f; 

    Vector3 randomOffset = Random.insideUnitSphere * maxDistance;
    randomOffset.y = 0; 

    Vector3 spawnPosition = player.position + randomOffset;

    spawnPosition.y = Terrain.activeTerrain.SampleHeight(spawnPosition);

    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
}
}
