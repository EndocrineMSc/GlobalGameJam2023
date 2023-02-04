using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    private void Start()
    {
        Init();
        ChangeState(EnemyState.WalkingUp);
    }


    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.WalkingUp:
                BasicWalkUp();
                break;
            case EnemyState.Attacking:
                BasicAttackRoutine();
                break;
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

        base.ChangeState(newState);
    }
}
