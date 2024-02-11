using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class E1 : Entity
{
    public E1IdleState E1IdleState { get; private set; }
    public E1MoveState E1MoveState { get; private set; }
    public E1DetectionState E1DetectionState { get; private set; }
    public E1ChargeState E1ChargeState { get; private set; }
    public E1LookForPlayerState E1LookForPlayerState { get; private set; }
    public E1MeleeAttackState E1MeleeAttackState { get; private set; }
    public E1StunState E1StunState { get; private set; }
    public E1DeadState E1DeadState { get; private set; }

    [SerializeField] private EnemyIdleData enemyIdleData;
    [SerializeField] private EnemyMoveData enemyMoveData;
    [SerializeField] private EnemyDetectionData enemyDetectionData;
    [SerializeField] private EnemyChargeData enemyChargeData;
    [SerializeField] private EnemyLookForPlayerData enemyLookForPlayerData;
    [SerializeField] private EnemyMeleeAttackData enemyMeleeAttackData;
    [SerializeField] private EnemyStunData enemyStunData;
    [SerializeField] private EnemyDeadData enemyDeadData; 
    [SerializeField] private Transform meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();
        
        E1MoveState = new E1MoveState(this, StateMachine, "move", enemyMoveData, this);
        E1IdleState = new E1IdleState(this, StateMachine, "idle", enemyIdleData, this);
        E1DetectionState =  new E1DetectionState(this, StateMachine, "detected", enemyDetectionData, this); 
        E1ChargeState =  new E1ChargeState(this, StateMachine, "charge", enemyChargeData, this);
        E1LookForPlayerState = new E1LookForPlayerState(this, StateMachine, "lookForPlayer", enemyLookForPlayerData, this);
        E1MeleeAttackState = new E1MeleeAttackState(this, StateMachine, "melee", meleeAttackPosition, enemyMeleeAttackData, this);
        E1StunState = new E1StunState(this, StateMachine, "stun", enemyStunData, this);
        E1DeadState = new E1DeadState(this, StateMachine, "dead", enemyDeadData, this);

        stats.Poise.OnCurrentValueZero += HandlePoiseZero;

    }

    private void HandlePoiseZero() {
        StateMachine.ChangeState(E1StunState);
    }

    private void OnDestroy() {
        stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
    }

    private void Start() {  
        StateMachine.Initialize(E1MoveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, enemyMeleeAttackData.AttackRadius);
    }
}
