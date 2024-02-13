using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            // t Input on the x axis
            if (xInput != 0)
            {
                //t Switch to the move state
                stateMachine.ChangeState(player.MoveState);
            }
            
            else 
            {
                //FIXME - Movement?.SetVelocityX(0f);
                
                // t The animation ends and the trigger is set
                if(isAnimationFinished)
                {
                    // t Switch to the idle state when the land is finish
                    stateMachine.ChangeState(player.IdleState);
                }

            }
        }
    }

}
