using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField]
    protected EntityPool entityPool;
    [SerializeField]
    protected float attackCooldown = 2;
    [SerializeField]
    protected float attackSpeed;
    protected float timeSinceLastAttack = 0;

    protected int damage;
    protected int currentUpgradeLevel = 1;

    public virtual void Upgrade()
    {
        currentUpgradeLevel++;
    }
}
