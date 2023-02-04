using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Animal
{
    public GameObject nutPrefab;
    public Transform nutSpawnPoint;

    [SerializeField]
    HealthEntity currentTarget;

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack > attackCooldown)
        {
            Attack();
            timeSinceLastAttack = 0;
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();


        switch (currentUpgradeLevel)
        {
            case 1:
                damage = 1;
                break;
            case 2:
                damage = 2;
                break;
            case 3:
                damage = 3;
                break;
            case 4:
                damage = 4;
                break;
        }
    }

    void Attack()
    {
        if (currentTarget == null)
            FindNewTarget();
        if (currentTarget == null)
            return;

        Bullet newBullet = Instantiate(nutPrefab, nutSpawnPoint.position, Quaternion.identity).GetComponent<Bullet>();
        newBullet.SetDirection(currentTarget.transform.position - nutSpawnPoint.position);
        newBullet.damage = damage;
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
