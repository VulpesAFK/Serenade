using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilitiesState
{
    private int amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
        amountOfJumpsLeft = playerData.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        // t Set the use jump input to false as currently active
        player.InputHandler.UseJumpInput();
        // t Make the player move upwards with the added jump velocity 
        core.Movement.SetVelocityY(playerData.JumpVelocity);

        // t Set to true and allow for the abilities state to take over
        isAbilityDone = true;
        // t Decreased by one when they enter
        amountOfJumpsLeft --;

        // t Set to true to allow for the a possible variable jump height 
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        switch (amountOfJumpsLeft) {
            case > 0: return true;
            default:  return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.AmountOfJumps;


    // f Decrease the amount of jumps by one 
    public void DecreaseAmountOfJumpsLeft()
    {
        amountOfJumpsLeft --;
    }
}
