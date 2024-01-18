using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2 : Entity
{
    public E2MoveState E2MoveState { get; private set; }
    public E2IdleState E2IdleState { get; private set; }
    public E2DetectionState E2DetectionState { get; private set; }
    public E2MeleeState E2MeleeState { get; private set; }
    public E2LookForPlayerState E2LookForPlayerState { get; private set; }
    public E2StunState E2StunState { get; private set; }
    public E2DeadState E2DeadState { get; private set; }

    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private EnemyMoveData enemyMoveData;
    [SerializeField] private EnemyIdleData enemyIdleData;
    [SerializeField] private EnemyDetectionData enemyDetectionData;
    [SerializeField] private EnemyMeleeAttackData enemyMeleeAttackData;
    [SerializeField] private EnemyLookForPlayerData enemyLookForPlayerData;
    [SerializeField] private EnemyStunData enemyStunData;
    [SerializeField] private EnemyDeadData enemyDeadData;


    public override void Start() 
    {
        base.Start();

        E2MoveState = new E2MoveState(this, StateMachine, "move", enemyMoveData, this);
        E2IdleState = new E2IdleState(this, StateMachine, "idle", enemyIdleData, this);
        E2DetectionState = new E2DetectionState(this, StateMachine, "detection", enemyDetectionData, this);
        E2MeleeState = new E2MeleeState(this, StateMachine, "melee", meleeAttackPosition, enemyMeleeAttackData, this);
        E2LookForPlayerState = new E2LookForPlayerState(this, StateMachine, "lookForPlayer", enemyLookForPlayerData, this);
        E2StunState = new E2StunState(this, StateMachine, "stun", enemyStunData, this);
        E2DeadState = new E2DeadState(this, StateMachine, "dead", enemyDeadData, this);

        StateMachine.Initialize(E2IdleState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        
        if (isDead)
        {
            StateMachine.ChangeState(E2DeadState);
        }

        else if(isStunned && StateMachine.CurrentState != E2StunState)
        {
            StateMachine.ChangeState(E2StunState);
        }

        else if(!CheckPlayerInMinAggroRange())
        {
            E2LookForPlayerState.SetTurnImmediately(true);
            StateMachine.ChangeState(E2LookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, enemyMeleeAttackData.AttackRadius);
    }
}
