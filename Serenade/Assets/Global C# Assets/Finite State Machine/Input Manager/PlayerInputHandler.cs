using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }

    public int NormInputX { get; private set; }

    public int NormInputY { get; private set; }

    public bool JumpInput { get; private set; }

    public bool JumpInputStop { get; private set; }

    public bool GrabInput { get; private set; }


    public bool DashInput { get; private set; }

    public bool DashInputStop { get; private set; }

    public bool InteractInput { get; private set; }


    private float DashInputStartTime;

    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }

    public bool[] AttackInput { get; private set; }
    

    private void Start() 
    {
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInput = new bool[count];
    }

    // f Function storing the necessary processes for movement
    public void OnMoveInput(InputAction.CallbackContext context) {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnInteractInput(InputAction.CallbackContext context) {
        if (context.started) {
            InteractInput = true;
        }
        if (context.canceled) {
            InteractInput = false;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context) {
        if (context.started) {
            JumpInput = true;
            JumpInputStop = false;
        }

        if (context.canceled) {
            JumpInput = false;
            JumpInputStop = true;
        }
    }

    // F Function to store all logic for the dash input
    public void OnDashInput(InputAction.CallbackContext context)
    {
        // t The moment the dsah input is pressed down
        if (context.started)
        {
            // t Input has been pressed on
            DashInput = true;
            DashInputStop = false;
        }

        // t When the dash is released
        else if (context.canceled)
        {
            // t Set bool to false when the button has been released
            DashInputStop = true;
            DashInput = false;
        }
    }

    // f Function to work out the direction of the dash
    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        if (playerInput.currentControlScheme == "Keyboard Mouse")
        {
            RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        }

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }



    // f Function to chceck if the player is grabbing the wall 
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        // t Set true as the player presses the grab input 
        if (context.started)
        {
            GrabInput = true;
        }

        // t Set false when the player cancels the hold of the button
        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput[(int)CombatInputs.primary] = true;
        }

        if (context.canceled)
        {
            AttackInput[(int)CombatInputs.primary] = false;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput[(int)CombatInputs.secondary] = true;
        }

        if (context.canceled)
        {
            AttackInput[(int)CombatInputs.secondary] = false;
        }
    }

    // f Function to externally set the jump input off and to be used for other states
    public void UseJumpInput()
    {
        JumpInput = false;
    }
    public void UseInteractInput()
    {
        InteractInput = false;
    }

    // f Function used to allow for all dash users to set input false
    public void UseDashInput()
    {
        DashInput = false;
    }
}

public enum CombatInputs
{
    primary,
    secondary
}
