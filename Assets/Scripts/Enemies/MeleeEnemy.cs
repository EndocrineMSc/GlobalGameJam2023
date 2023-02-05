using GameName.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumCollection;

public class MeleeEnemy : Enemy
{
    public SFX[] enemyDeathSound;

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
        if (target.IsAlive)
        {
            base.Attack();

            target.Hit(damage);
            ChangeState(EnemyState.Idle);
        }
    }

    public override void Kill()
    {
        PlayEnemyDeathSound();
        base.Kill();
    }

    void PlayEnemyDeathSound()
    {
        if (AudioManager.Instance == null)
            return;

        EnumCollection.SFX soundEffect = enemyDeathSound[Random.Range(0, enemyDeathSound.Length)];

        AudioManager.Instance.PlaySoundEffect(soundEffect);
    }

    protected override void ChangeState(EnemyState newState)
    {

        base.ChangeState(newState);
    }
}
