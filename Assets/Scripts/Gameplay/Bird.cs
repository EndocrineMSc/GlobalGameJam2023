using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private Transform leftBound;
    [SerializeField]
    private Transform rightBound;
    [SerializeField]
    EntityPool enemyPool;

    [SerializeField]
    private float flySpeed;
    [SerializeField]
    private float attackSpeed;
    public float attackCooldown = 2;

    EnemyState currentState;
    AttackDirektion movementDirection;
    Vector3 movementPosition;
    float timeSinceLastAttack = 0;

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
                if (timeSinceLastAttack > attackCooldown)
                {
                    attackPosition = enemyPool.GetNextTarget().transform.position;
                    currentState = EnemyState.Attacking;
                }
                break;
            case EnemyState.Attacking:
                Attack();
                break;
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
    }
}
