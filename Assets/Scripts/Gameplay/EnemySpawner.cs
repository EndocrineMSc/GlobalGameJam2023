using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyToSpawn;
    [SerializeField]
    float spawnCooldown = 2;
    [SerializeField]
    Transform spawnTranform;
    [SerializeField]
    float yOffsetAccuracy = 0;

    float timeSinceLastSpawn = 0;


    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnCooldown)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = ((spawnTranform != null) ? spawnTranform.position : transform.position) + Random.Range(-yOffsetAccuracy, yOffsetAccuracy) * Vector3.up;
        // Try to spawn the new enemy at the spawn point if it is not null.
        Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
    }
}
