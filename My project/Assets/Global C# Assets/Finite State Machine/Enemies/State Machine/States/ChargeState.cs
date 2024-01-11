using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : EnemyState
{
    protected EnemyChargeData stateData;
    public ChargeState(Entity entity, EnemyStateMachine stateMachine, string animBoolName, EnemyChargeData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
}
