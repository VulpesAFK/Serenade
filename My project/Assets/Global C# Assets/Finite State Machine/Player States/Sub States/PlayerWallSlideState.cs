using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            // Slow the descent downwards
            Movement?.SetVelocityY(-playerData.WallSlideVelocity);

            if (grabInput && yInput == 0) { stateMachine.ChangeState(player.WallGrabState); }

        }

    }
}
