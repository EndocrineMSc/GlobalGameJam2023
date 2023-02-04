using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AttackDirektion {Left, Right }
public enum EnemyState {WalkingUp, Attacking, Idle }

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : HealthEntity
{
    [Header("Enemy")]
    public UnityEvent<EnemyState> OnStateChange;

    public float walkingSpeed;

    [Header("Attack")]
    public AttackDirektion attackDirection;
    public int damage;
    public float attackCooldown;
    [Tooltip("The distance needed to change from walking to attacking.")]
    public float attackDistance;
    public float attackDistanceAccuaracyOffset = 0.1f;

    protected EnemyState currentState;
    protected float timeSinceLastAttack;
    protected Rigidbody2D rigi2D;
    [SerializeField]
    protected HealthEntity target;

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        attackDistance += Random.Range(-attackDistanceAccuaracyOffset, attackDistanceAccuaracyOffset);
        attackDirection = (transform.position.x < 0) ? AttackDirektion.Left : AttackDirektion.Right;
        target = (attackDirection == AttackDirektion.Left) ? TreeBark.leftTreebark : TreeBark.rightTreebark;

        if (target == null)
            Debug.LogError("Enemy could not find the right target.");

        rigi2D = GetComponent<Rigidbody2D>();
        attackDirection = (transform.position.x < 0) ? AttackDirektion.Left : AttackDirektion.Right;
        base.Init();
    }

    protected virtual void ChangeState(EnemyState newState)
    {
        // Reset any speed.
        if (currentState == EnemyState.WalkingUp)
            rigi2D.velocity = Vector2.zero;

        currentState = newState;

        if(newState == EnemyState.WalkingUp)
            rigi2D.velocity = ((attackDirection == AttackDirektion.Left) ? Vector2.right : Vector2.left) * walkingSpeed;

        if (OnStateChange != null)
            OnStateChange.Invoke(newState);
    }

    protected void BasicWalkUp()
    {
        // Detect if the melee enemy is close to the target.
        if (Mathf.Abs(transform.position.x - target.transform.position.x) < attackDistance)
        {
            ChangeState(EnemyState.Idle);
        }
    }

    protected void IdleWaitForAttack()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack >= attackCooldown)
        {
            timeSinceLastAttack = 0;
            ChangeState(EnemyState.Attacking);
            Attack();
        }
    }

    protected virtual void Attack()
    {
        timeSinceLastAttack = 0;
    }
}
   