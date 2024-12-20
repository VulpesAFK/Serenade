using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
         
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityZero();

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
        bool CrouchMovement = !isExitingState && xInput != 0;
        bool ReleaseFromCrouch = !isExitingState && !isTouchingCeiling && yInput != -1;

        if (CrouchMovement) stateMachine.ChangeState(player.CrouchMoveState);
        else if (ReleaseFromCrouch) stateMachine.ChangeState(player.IdleState);


        // if (!isExitingState)
        // {
        //     if (xInput != 0)
        //     {
        //         stateMachine.ChangeState(player.CrouchMoveState);
        //     }

        //     else if (yInput != -1 && !isTouchingCeiling)
        //     {
        //         stateMachine.ChangeState(player.IdleState);
        //     }
        // }
    }
}
