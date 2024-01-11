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

    [SerializeField] private EnemyIdleData enemyIdleData;
    [SerializeField] private EnemyMoveData enemyMoveData;
    [SerializeField] private EnemyDetectionData enemyDetectionData;
    [SerializeField] private EnemyChargeData enemyChargeData;
    [SerializeField] private EnemyLookForPlayerData enemyLookForPlayerData;

    public override void Start()
    {
        base.Start();
        
        E1MoveState = new E1MoveState(this, StateMachine, "move", enemyMoveData, this);
        E1IdleState = new E1IdleState(this, StateMachine, "idle", enemyIdleData, this);
        E1DetectionState =  new E1DetectionState(this, StateMachine, "detected", enemyDetectionData, this); 
        E1ChargeState =  new E1ChargeState(this, StateMachine, "charge", enemyChargeData, this);
        E1LookForPlayerState = new E1LookForPlayerState(this, StateMachine, "lookForPlayer", enemyLookForPlayerData, this);

        StateMachine.Initialize(E1MoveState);
    }
}
