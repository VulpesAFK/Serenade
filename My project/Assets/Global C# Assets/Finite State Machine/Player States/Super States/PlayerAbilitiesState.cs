using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesState : PlayerState
{
    // v Bools to help to save the abilties inheriting to be saved
    protected bool isAbilityDone;
    // v Check the surrounding made specifically the made
    private bool isGrounded;

    public PlayerAbilitiesState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }
    public override void DoChecks()
    {
        base.DoChecks();

        // t Base check from the ground bool
        isGrounded = core.Collision.Ground;
    }

    public override void Enter()
    {
        base.Enter();

        // t Set to false when the states starts
        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // t When the inheriting states finish will go back to the base
        if (isAbilityDone)
        {
            // t The player is grounded and a leeway on the y axis assuming still
            if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
            {
                // t Switch to idle state
                stateMachine.ChangeState(player.IdleState);
            }

            // t If the grounded then then player is most likely in the air
            else
            {
                //t Switch to the in air state
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
