using System.Collections;
using System.Collections.Generic;
using FoxTail;
using UnityEngine;

public class KnockBackReceiver : CoreComponent, IKnockBackable
{
    private bool isKnockBackActive;
    private float knockBackStartTime;
    private float maxKnockBackTime = 0.2f;

    private CoreComp<Movement> movement;
    private CoreComp<Collision> collision;

    protected override void Awake()
    {
        base.Awake();

        movement = new CoreComp<Movement>(core);
        collision = new CoreComp<Collision>(core);
    }

    public override void LogicUpdate()
    {
        CheckKnockBack();
    }

    public void KnockBack(Vector2 angle, float strength, int direction)
    {
        movement.Component?.SetVelocity(strength, angle, direction);
        movement.Component.CanSetVelocity = false;

        isKnockBackActive = true;
        knockBackStartTime = Time.time;
    }

    private void CheckKnockBack()
    {
        if (isKnockBackActive && ((movement.Component?.CurrentVelocity.y <= 0.01 && collision.Component.Ground) || Time.time >= knockBackStartTime + maxKnockBackTime))
        {
            isKnockBackActive = false;
            movement.Component.CanSetVelocity = true;
        }
    }
}
