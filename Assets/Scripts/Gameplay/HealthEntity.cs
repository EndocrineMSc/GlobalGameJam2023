using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A basic script that give an object health pool and makes it able to take damage and die.
/// </summary>
public class HealthEntity : MonoBehaviour
{
    /// <summary>
    /// Gets called when die enemy dies.
    /// </summary>
    public UnityEvent<HealthEntity> OnDeath;

    [SerializeField]
    protected int maxLive;
    [SerializeField]
    protected int currentLive;

    protected virtual void Init()
    {
        currentLive = maxLive;
    }

    public bool IsAlive
    {
        get { return currentLive > 0; }
    }

    public virtual void Hit(int damage)
    {
        currentLive -= damage;

        if (currentLive <= 0)
            Kill();
    }

    public virtual void Kill()
    {
        if (OnDeath != null)
            OnDeath.Invoke(this);

        Destroy(this.gameObject);
    }
}
