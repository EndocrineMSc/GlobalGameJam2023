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

            PlayEnemyDeathSound();
            target.Hit(damage);
        }
    }

    public override void Kill()
    {
        AudioManager.Instance.PlaySoundEffect(EnumCollection.SFX.SFX_015_Enemy_Death1);
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
