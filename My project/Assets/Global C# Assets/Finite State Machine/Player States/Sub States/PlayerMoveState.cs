using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // t Check if the sprite needs to be flipped
        core.Movement.CheckIfShouldFlip(xInput);
        // t Move the x axis to the player with the velocity
        core.Movement.SetVelocityX(playerData.MovementVelocity * xInput);

        if (!isExitingState)
        {
            // t If the player input is null and there is no manual exit
            if (xInput == 0) 
            {
                // t Switch to the idle state
                stateMachine.ChangeState(player.IdleState);
            }
            else if (yInput == -1)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
