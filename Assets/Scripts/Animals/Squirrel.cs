using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameName.Audio;

public class Squirrel : Animal
{
    public GameObject nutPrefab;
    public Transform nutSpawnPoint;

    [SerializeField]
    HealthEntity currentTarget;

    private void Start()
    {
        damage = 1;
    }

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
                damage = 2;
                attackCooldown = 1.5f;
                break;
            case 2:
                damage = 3;
                attackCooldown = 2;
                attackCooldown = 1.15f;
                break;
            case 3:
                damage = 4;
                attackCooldown = 1f;
                break;
            case 4:
                damage = 5;
                attackCooldown = 0.75f;
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

        AudioManager.Instance.PlaySoundEffect(EnumCollection.SFX.SFX_033_SquirrelAttack1);
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
