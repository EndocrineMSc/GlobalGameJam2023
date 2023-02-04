using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class pooling the enemies in a certain range.
/// </summary>
public class EntityPool : MonoBehaviour
{

    public
    List<HealthEntity> pool;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        HealthEntity healthEnt = collision.gameObject.GetComponent<HealthEntity>();

        if (healthEnt != null)
        {
            AddToPool(healthEnt);
        }
    }

    void AddToPool(HealthEntity newHealthEntity)
    {
        pool.Add(newHealthEntity);
        newHealthEntity.OnDeath.AddListener(EntityDied);
    }

    public HealthEntity GetNextTarget()
    {
        if (pool.Count > 0)
            // TODO: make more robust. Ensure no null.
            return pool[0];
        else
            return null;
    }

    private void EntityDied(HealthEntity healthEntity)
    {
        if (pool.Contains(healthEntity))
            pool.Remove(healthEntity);
    }

}
