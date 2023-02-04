using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOnCollision : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthEntity healthEnt = collision.gameObject.GetComponent<HealthEntity>();

        if (healthEnt != null)
        {
            healthEnt.Hit(damage);
        }
    }
}
