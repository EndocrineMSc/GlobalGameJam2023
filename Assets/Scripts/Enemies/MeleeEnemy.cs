using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MeleeEnemy : Enemy
{
    public float speed = 2f;

    [SerializeField]
    HealthEntity target;
    Rigidbody2D rigi2D;


    private void Start()
    {
        target = (attackDirection == AttackDirektion.Left) ? TreeBark.leftTreebark : TreeBark.rightTreebark;

        if (target == null)
            Debug.LogError("Enemy could not find the right target.");

        rigi2D = GetComponent<Rigidbody2D>();

        Init();
    }


    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.WalkingUp:
                WalkUp();
                break;
            case EnemyState.Attacking:
                BasicAttackRoutine();
                break;
        }
    }

    void WalkUp()
    {
        rigi2D.velocity = ((attackDirection == AttackDirektion.Left) ? Vector2.right : Vector2.left) * speed * Time.deltaTime;

        // Detect if the melee enemy is close to the target.
        if (Mathf.Abs(transform.position.x - target.transform.position.x) < 1f)
        {
            Debug.Log("close");
            ChangeState(EnemyState.Attacking);
        }
    }

    protected override void Attack()
    {
        if (target.IsAlive)
        {
            base.Attack();

            target.Hit(damage);
        }
    }

    protected override void ChangeState(EnemyState newState)
    {
        if (currentState == EnemyState.WalkingUp)
            rigi2D.velocity = Vector2.zero;

        base.ChangeState(newState);
    }
}
