using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float explosionRange = 2;
    [SerializeField]
    GameObject explodeParticles;
    [SerializeField]
    LayerMask enemyLayerMask;
    [SerializeField]
    float explosionDelay = 2;

    private void Start()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRange, Vector2.right * 0.01f, enemyLayerMask);

        foreach (RaycastHit2D hit in hits)
        {
            HealthEntity enemy = hit.collider.gameObject.GetComponent<HealthEntity>();

            if (enemy != null)
                enemy.Hit(damage);
        }

        Instantiate(explodeParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
