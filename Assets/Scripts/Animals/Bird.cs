using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameName.Audio;

public class Bird : Animal
{
    [SerializeField]
    private Transform leftBound;
    [SerializeField]
    private Transform rightBound;

    [SerializeField]
    private float flySpeed;
    [SerializeField]
    HitOnCollision damageCollider;

    EnemyState currentState;
    AttackDirektion movementDirection;
    Vector3 movementPosition;

    Vector3 attackPosition;

    private void Start()
    {
        currentState = EnemyState.WalkingUp;
        movementPosition = leftBound.position;
    }

    private void Update()
    {
        UpdateMovementPosition();

        timeSinceLastAttack += Time.deltaTime;

        switch (currentState)
        {
            case EnemyState.WalkingUp:
                Fly();
                CheckForAttack();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();

        switch (currentUpgradeLevel)
        {
            case 1:
                damageCollider.damage = 2;
                attackCooldown = 2.75f;
                attackSpeed = 9;
                break;
            case 2:
                damageCollider.damage = 3;
                attackCooldown = 2.5f;
                attackSpeed = 10;
                break;
            case 3:
                damageCollider.damage = 4;
                attackCooldown = 2.25f;
                attackSpeed = 11;
                break;
            case 4:
                damageCollider.damage = 5;
                attackCooldown = 2f;
                attackSpeed = 12;
                break;
        }

    }

    void CheckForAttack()
    {
        if (timeSinceLastAttack > attackCooldown)
        {
            HealthEntity target = entityPool.GetNextTarget();
            if (target != null)
            {
                Vector2 enemyMovementOffset = target.gameObject.GetComponent<Rigidbody2D>().velocity;
                attackPosition = target.gameObject.transform.position + Vector3.right * enemyMovementOffset.x;
                currentState = EnemyState.Attacking;
            }
        }
    }

    void Fly()
    {
        if ((transform.position - movementPosition).sqrMagnitude > 0.05f)
        {
            transform.position = transform.position + (movementPosition - transform.position).normalized * flySpeed * 2f * Time.deltaTime;
        }
        else
            transform.position = movementPosition;

    }

    void UpdateMovementPosition()
    {
        if (movementDirection == AttackDirektion.Left)
        {
            movementPosition += (leftBound.position - movementPosition).normalized * flySpeed * Time.deltaTime;
            if ((leftBound.position - movementPosition).sqrMagnitude < 0.2f)
                movementDirection = AttackDirektion.Right;

        }
        else
        {
            movementPosition += (rightBound.position - movementPosition).normalized * flySpeed * Time.deltaTime;
            if ((rightBound.position - movementPosition).sqrMagnitude < 0.2f)
                movementDirection = AttackDirektion.Left;
        }
    }

    void Attack()
    {
        transform.position = transform.position + (attackPosition - transform.position).normalized * attackSpeed * Time.deltaTime;

        if ((transform.position - attackPosition).sqrMagnitude < 0.05f)
        {
            currentState = EnemyState.WalkingUp;
            timeSinceLastAttack = 0;
        }

        AudioManager.Instance.PlaySoundEffect(EnumCollection.SFX.SFX_026_BirdAttack1);
    }
}
