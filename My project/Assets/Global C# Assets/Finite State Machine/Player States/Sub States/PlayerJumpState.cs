using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilitiesState
{
    // v Variable to hold the amount of possible jumps left 
    private int amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        // t Instantiate the amount of jumps left to to the lax number of jumps
        amountOfJumpsLeft = playerData.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        // t Set the use jump input to false as currently active
        player.InputHandler.UseJumpInput();
        // t Make the player move upwards with the added jump velocity 
        player.SetVelocityY(playerData.JumpVelocity);

        // t Set to true and allow for the abilities state to take over
        isAbilityDone = true;
        // t Decreased by one when they enter
        amountOfJumpsLeft --;

        // t Set to true to allow for the a possible variable jump height 
        player.InAirState.SetIsJumping();
    }

    // f Allow the bool conditions to be able to continue jumping in the air 
    public bool CanJump()
    {
        // t If there is jumps left then return true
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }

        // t False when the player jump amount is zero or below
        else
        {
            return false;
        }

    }

    // f Reset the amount of jumps back to the max
    public void ResetAmountOfJumpsLeft()
    {
        amountOfJumpsLeft = playerData.AmountOfJumps;
    }


    // f Decrease the amount of jumps by one 
    public void DecreaseAmountOfJumpsLeft()
    {
        amountOfJumpsLeft --;
    }
}
