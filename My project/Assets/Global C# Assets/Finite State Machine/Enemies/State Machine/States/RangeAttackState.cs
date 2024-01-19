using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected EnemyRangeAttackData stateData;
    protected GameObject projectile;
    protected Projectile projectileScript;
    public RangeAttackState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, Transform attackPosition, EnemyRangeAttackData stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    { 
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        projectile = GameObject.Instantiate(stateData.Projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(stateData.ProjectileVelocity, stateData.ProjectileTravelDistance, stateData.ProjectileDamage);

    }
}
