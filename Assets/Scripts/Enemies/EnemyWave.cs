using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A wave has all the details for the whole wave.
/// </summary>
[System.Serializable]
public class EnemyWave
{
    public float spawnCooldown = 0.5f;
    public EnemySet[] enemySets = new EnemySet[1];
}

/// <summary>
/// An enemy set describes what kind of enemy come from what direction an how many.
/// </summary>
[System.Serializable]
public class EnemySet
{
    public EnemyType enemyType;
    public AttackDirektion direction;
    public int amount = 1;

    public EnemySet(EnemyType enemyType, AttackDirektion direction)
    {
        this.enemyType = enemyType;
        this.direction = direction;
    }
}
