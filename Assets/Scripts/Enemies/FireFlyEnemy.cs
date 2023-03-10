using GameName.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A firefly enemy is an enemy that flies towars the tree.
/// When in attack range it will hover for a short moment before crashing into the treebark.
/// </summary>
public class FireFlyEnemy : Enemy
{
    [SerializeField]
    float attackSpeed = 10;

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
            case EnemyState.Idle:
                IdleWaitForAttack();
                break;
        }
    }

    protected override void Attack()
    {
        AudioManager.Instance.PlaySoundEffect(EnumCollection.SFX.SFX_012_Enemy_Attack3);
        rigi2D.velocity = (target.transform.position - transform.position).normalized * attackSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TreeBark"))
        {
            AudioManager.Instance.PlaySoundEffect(EnumCollection.SFX.SFX_020_Tree_Damage1);

            target.Hit(damage);
            // Kill onself after crashing.
            Kill();
        }
    }
}
