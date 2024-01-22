using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    private bool isKnockbackActive;
    private float knockbackStartTime;
    private float maxKnockBackTime = 0.2f;

    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
    private Collision collision;
    private Stats Stats { get => stats ??= core.GetCoreComponent<Stats>(); }
    private Stats stats;


    public override void LogicUpdate()
    {
        CheckKnockback();
    }
    public void Damage(float amount)
    {
        Debug.Log($"{core.transform.parent.name} has been damaged");
        Stats?.DecreaseHealth(amount);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        Movement?.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;

        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && ((Movement?.CurrentVelocity.y <= 0.01 && Collision.Ground) || Time.time >= knockbackStartTime + maxKnockBackTime))
        {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
}
