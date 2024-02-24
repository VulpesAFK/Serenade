using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

    public override void Enter() {
        base.Enter();

        Movement?.SetVelocityX(0f);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // Conditions
        bool conditionalMovement = !isExitingState && xInput != 0;
        bool conditionalCrouch = !isExitingState && yInput == -1;

        if (conditionalMovement) stateMachine.ChangeState(player.MoveState);
        else if (conditionalCrouch) stateMachine.ChangeState(player.CrouchIdleState);
    }
}
