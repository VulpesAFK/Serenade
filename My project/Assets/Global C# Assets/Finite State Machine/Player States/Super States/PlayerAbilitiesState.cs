using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesState : PlayerState
{
    // Bools to help to save the abilties inheriting to be saved
    protected bool isAbilityDone;

    // Check the surrounding made specifically the made
    private bool isGrounded;

    public PlayerAbilitiesState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {

    }

    public override void DoChecks() {
        base.DoChecks();

        isGrounded = core.Collision.Ground;
    }

    public override void Enter() {
        base.Enter();

        // Return control to state when any ability inheriting is finish
        isAbilityDone = false;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (isAbilityDone)
        {
            if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f) { stateMachine.ChangeState(player.IdleState); }

            else { stateMachine.ChangeState(player.InAirState); }
        }
    }
}
