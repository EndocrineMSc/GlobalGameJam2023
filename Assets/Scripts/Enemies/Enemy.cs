using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AttackDirektion {Left, Right }
public enum EnemyState {WalkingUp, Attacking, Idle }

public class Enemy : HealthEntity
{
    public UnityEvent<EnemyState> OnStateChange;

    public AttackDirektion attackDirection;
    public int damage;
    public float attackCooldown;

    protected EnemyState currentState;
    protected float timeSinceLastAttack;

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        attackDirection = (transform.position.x < 0) ? AttackDirektion.Left : AttackDirektion.Right;
        base.Init();
    }

    protected virtual void ChangeState(EnemyState newState)
    {
        currentState = newState;

        if (OnStateChange != null)
            OnStateChange.Invoke(newState);
    }

    protected void BasicAttackRoutine()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack >= attackCooldown)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        timeSinceLastAttack = 0;
    }
}
   