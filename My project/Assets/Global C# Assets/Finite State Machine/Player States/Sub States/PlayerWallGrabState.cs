using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.FiniteStateMachine.Construct;
using FoxTail.Serenade.Experimental.FiniteStateMachine.SuperStates;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.SubStates
{
    public class PlayerWallGrabState : PlayerTouchingWallState
    {
        // v Variable holding the vector2 position from when it started
        private Vector2 holdPosition;

        public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
            
        }

        public override void Enter()
        {
            base.Enter();

            // t Store the initial position from the state when entered
            holdPosition = player.transform.position;

            HoldPosition();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // // t Check whether we are not exiting from the state 
            // if (!isExitingState)
            // {
            //     // t Hold the position of the player to the wall
            //     HoldPosition();
                
            //     // t If input is going upwards
            //     if (yInput > 0.0f)
            //     {
            //         // t Switch to the wall climb state
            //         // stateMachine.ChangeState(player.WallClimbState);
            //     }

            //     // t If there is no y input and not grabbing
            //     else if (yInput < 0.0f || !grabInput)
            //     {
            //         // t Switch to the wall slide state 
            //         // stateMachine.ChangeState(player.WallSlideState);
            //     }

            if (!isExitingState) HoldPosition();
        }

        // f Hold the position of the player 
        private void HoldPosition() {
            // t Force the position to be the stored positions
            player.transform.position = holdPosition;

            // t Set forces to zero to remove any other external forces 
            Movement?.SetVelocityX(0f);
            Movement?.SetVelocityY(0f);
        }
    }
}
