using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class pooling the enemies in a certain range.
/// </summary>
public class EntityPool : MonoBehaviour
{

    public float poolingDistance = 5;

    List<HealthEntity> pool;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        HealthEntity healthEnt = collision.gameObject.GetComponent<HealthEntity>();

        if (healthEnt != null)
        {
            pool.Add(healthEnt);
        }
    }

    public Transform GetNextTarget()
    {
        // TODO: make more robust. Ensure no null.
        return pool[0].transform;
    }

    private void EntityDied(HealthEntity healthEntity)
    {
        if (pool.Contains(healthEntity))
            pool.Remove(healthEntity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, poolingDistance);
    }
}
