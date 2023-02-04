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
    Transform spawnPos;

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
        // Try to spawn the new enemy at the spawn point if it is not null.
        Instantiate(enemyToSpawn, (spawnPos != null) ? spawnPos.position : transform.position, Quaternion.identity);
    }
}
