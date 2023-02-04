using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A bullet is anything projectile intented to hit a health entity.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthEntity healthEnt = collision.gameObject.GetComponent<HealthEntity>();

        if (healthEnt != null)
        {
            healthEnt.Hit(damage);
            Destroy(this.gameObject);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;
    }
}
