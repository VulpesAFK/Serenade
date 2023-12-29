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

        // t If the grounded state being inherited is not in an exiting state
        if (!isExitingState)
        {
            // t Input on the x axis
            if (xInput != 0)
            {
                //t Switch to the move state
                stateMachine.ChangeState(player.MoveState);
            }
    
            // t The animation ends and the trigger is set
            else if(isAnimationFinished)
            {
                // t Switch to the idle state when the land is finish
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

}
