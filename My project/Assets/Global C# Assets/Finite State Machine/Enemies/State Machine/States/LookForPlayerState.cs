using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : EnemyState
{
    protected EnemyLookForPlayerData stateData;

    protected bool turnImmediately;
    protected bool isPlayerInMinAggroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;

    protected int amountOfTurnsDone;

    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;

    public LookForPlayerState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyLookForPlayerData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isAllTurnsDone =  false;
        isAllTurnsTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnsDone = 0;

        Movement?.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(0f);

        if (turnImmediately)
        {
            Movement?.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }
        else if (Time.time >= lastTurnTime + stateData.TimeBetweenTurns && !isAllTurnsDone)
        {
            Movement?.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone ++;
        }

        if (amountOfTurnsDone >= stateData.AmountOfTurns)
        {
            isAllTurnsDone = true;
        }

        if (Time.time >= lastTurnTime + stateData.TimeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTurnImmediately(bool turn)
    {
        turnImmediately = turn;
    }
}
