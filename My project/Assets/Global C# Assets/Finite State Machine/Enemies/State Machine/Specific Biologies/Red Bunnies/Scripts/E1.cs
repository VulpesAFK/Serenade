using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class E1 : Entity
{
    public E1IdleState E1IdleState { get; private set; }
    public E1MoveState E1MoveState { get; private set; }

    [SerializeField] private EnemyIdleData enemyIdleData;
    [SerializeField] private EnemyMoveData enemyMoveData;

    public override void Start()
    {
        base.Start();
        
        E1MoveState = new E1MoveState(this, StateMachine, "move", enemyMoveData, this);
        E1IdleState = new E1IdleState(this, StateMachine, "idle", enemyIdleData, this);

        StateMachine.Initialize(E1MoveState);
    }
}
