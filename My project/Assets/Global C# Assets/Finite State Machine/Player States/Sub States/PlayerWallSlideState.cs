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

        // t If the state is not exiting
        if (!isExitingState)
        {
            // t Slow the player down when it is going down
            core.Movement.SetVelocityY(-playerData.WallSlideVelocity);

            // t If the player is going to be start grabbing and the input on the y axis is not on
            if (grabInput && yInput == 0)
            {
                // t Switch the state
                stateMachine.ChangeState(player.WallGrabState);
            }

        }

    }
}
