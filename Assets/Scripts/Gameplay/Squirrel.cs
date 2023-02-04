using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : MonoBehaviour
{
    public float attackCooldown = 2;
    public GameObject nutPrefab;
    public Transform nutSpawnPoint;
    public EntityPool entityPool;

    [SerializeField]
    HealthEntity currentTarget;
    float timeSinceLastThrow;

    private void Update()
    {
        timeSinceLastThrow += Time.deltaTime;

        if (timeSinceLastThrow > attackCooldown)
        {
            Attack();
            timeSinceLastThrow = 0;
        }
    }

    void Attack()
    {
        if (currentTarget == null)
            FindNewTarget();
        if (currentTarget == null)
            return;

        GameObject newNut = Instantiate(nutPrefab, nutSpawnPoint.position, Quaternion.identity);
        newNut.GetComponent<Bullet>().SetDirection(currentTarget.transform.position - nutSpawnPoint.position);
    }

    void FindNewTarget()
    {
        currentTarget = entityPool.GetNextTarget();
        if(currentTarget != null)
            currentTarget.OnDeath.AddListener(OnTargetDie);
    }

    void OnTargetDie(HealthEntity deadEntity)
    {
        FindNewTarget();
    }
}
