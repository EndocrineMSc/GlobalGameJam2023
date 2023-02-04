using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    private void Start()
    {
        attackDistance += Random.Range(-0.1f, 0.1f);
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
            case EnemyState.Idle:
                IdleWaitForAttack();
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
