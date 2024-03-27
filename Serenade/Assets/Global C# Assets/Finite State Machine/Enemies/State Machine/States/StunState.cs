using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : EnemyState
{
    protected EnemyStunData stateData;
    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAggroRange;

    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;
    private Collision Collision { get => collision ??= core.GetCoreComponent<Collision>(); }
    private Collision collision;


    public StunState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyStunData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = Collision.Ground;
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isMovementStopped = false;
        isStunTimeOver = false;
        Movement?.SetVelocity(stateData.StunKnockBackVelocity, stateData.StunKnockBackAngle, entity.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.StunTime)
        {
            isStunTimeOver = true;
        }

        if(isGrounded && Time.time >= startTime + stateData.StunKnockBackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            Movement?.SetVelocityX(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
