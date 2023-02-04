using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    Animator anim;
    Enemy enemy;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponentInParent<Enemy>();

        enemy.OnStateChange.AddListener(OnStateChange);

        // Flip sprite depending on what side the enemy is on.
        Vector3 localScale = transform.localScale;
        localScale.x *= (transform.position.x > 0) ? 1 : -1;
        transform.localScale = localScale;
    }

    void OnStateChange(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Idle:
                anim.SetBool("Walking", false);
                break;
            case EnemyState.WalkingUp:
                anim.SetBool("Walking", true);
                break;
            case EnemyState.Attacking:
                anim.SetTrigger("Attack");
                break;
        }
    }

}
