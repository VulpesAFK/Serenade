using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates
{
    public class PlayerWallClimbState : PlayerTouchingWallState
    {
        public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerStateData playerStateData) : base(player, stateMachine, playerData, animBoolName, playerStateData)
        {
            
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // t Not finishing the state 
            if (!isExitingState)
            {
                // t Set the climbing velocity upwards
                Movement?.SetVelocityY(playerData.WallClimbVelocity);

                // t If the player is not wanting to go upwards
                if (yInput != 1)
                {
                    // t Switch the player to the grab state
                    // stateMachine.ChangeState(player.WallGrabState);
                }
            }

        }
    }
}
