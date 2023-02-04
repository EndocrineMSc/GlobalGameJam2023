using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A bullet is anything projectile intented to hit a health entity.
/// </summary>
public class Bullet : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthEntity healthEnt = collision.gameObject.GetComponent<HealthEntity>();

        if (healthEnt != null)
        {
            healthEnt.Hit(damage);
            Destroy(this.gameObject);
        }
    }
}
