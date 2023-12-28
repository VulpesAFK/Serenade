using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // t Not finishing the state 
        if (!isExitingState)
        {
            // t Set the climbing velocity upwards
            core.Movement.SetVelocityY(playerData.WallClimbVelocity);

            // t If the player is not wanting to go upwards
            if (yInput != 1)
            {
                // t Switch the player to the grab state
                stateMachine.ChangeState(player.WallGrabState);
            }
        }

    }
}
