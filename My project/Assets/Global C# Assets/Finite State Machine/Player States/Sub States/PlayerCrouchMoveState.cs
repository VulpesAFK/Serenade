using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.SetColliderHeight(playerData.CrouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();

        player.SetColliderHeight(playerData.StandColliderHeight);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Conditions
        bool StopMovement = !isExitingState && xInput == 0;
        bool NonCrouchMovement = !isExitingState && yInput != -1 && !isTouchingCeiling;

        if (!isExitingState)
        {
            core.Movement.SetVelocityX(playerData.CrouchMovementVelocity * core.Movement.FacingDirection);
            core.Movement.CheckIfShouldFlip(xInput);
        }

        if (StopMovement) stateMachine.ChangeState(player.CrouchIdleState);
        else if (NonCrouchMovement) stateMachine.ChangeState(player.MoveState);

            // if (xInput == 0)
            // {
            //     stateMachine.ChangeState(player.CrouchIdleState);
            // }

            // else if (yInput != -1 && !isTouchingCeiling)
            // {
            //     stateMachine.ChangeState(player.MoveState);
            // }
    }
}
