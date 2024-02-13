using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    // Raw vector2 of the current movement input
    public Vector2 RawMovementInput { get; private set; }
    // Normalized input of the movement input via the x axis
    public int NormInputX { get; private set; }
    // Normalized input of the movement input via the y axis
    public int NormInputY { get; private set; }
    // Bool for the jump input pressed down
    public bool JumpInput { get; private set; }
    // Bool for the jump input when it's placed up 
    public bool JumpInputStop { get; private set; }
    // Bool to store the grab input 
    public bool GrabInput { get; private set; }

    // Bool storing the dash input
    public bool DashInput { get; private set; }
    // Bool when the dash input is pressed up
    public bool DashInputStop { get; private set; }

    // Time for when the dash is used
    private float DashInputStartTime;

    // Position angle for dash
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

    // Function storing the necessary processes for movement
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // Read the raw movement vector2 from keyboard or controller input
        RawMovementInput = context.ReadValue<Vector2>();

        // Provide a threshold for the player input via the x axis
        NormInputX = Mathf.RoundToInt(RawMovementInput.x);

        // Provide a threshold for the player input via the y axis
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);

    }

    // Function to store all logic for the jump input
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // Check if the player has pressed the space or controller button
        if (context.started)
        {
            // Declare the jump input to be true
            JumpInput = true;
            // Declare when the released
            JumpInputStop = false;
        }

        // Check if the player has relased the space or controller button
        if (context.canceled)
        {
            // Switch bool to false as jump input is off
            JumpInput = false;
            // Short pressed jump input has been set true
            JumpInputStop = true;
        }
    }

    // Function to store all logic for the dash input
    public void OnDashInput(InputAction.CallbackContext context)
    {
        // The moment the dsah input is pressed down
        if (context.started)
        {
            // Input has been pressed on
            DashInput = true;
            DashInputStop = false;
        }

        // When the dash is released
        else if (context.canceled)
        {
            // Set bool to false when the button has been released
            DashInputStop = true;
            DashInput = false;
        }
    }

    // Function to work out the direction of the dash
    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        if (playerInput.currentControlScheme == "Keyboard Mouse")
        {
            RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        }

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }



    // Function to chceck if the player is grabbing the wall 
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        // Set true as the player presses the grab input 
        if (context.started)
        {
            GrabInput = true;
        }

        // Set false when the player cancels the hold of the button
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

    // Function to externally set the jump input off and to be used for other states
    public void UseJumpInput()
    {
        JumpInput = false;
    }

    // Function used to allow for all dash users to set input false
    public void UseDashInput()
    {
        DashInput = false;
    }

    public void UseAttackInput(int i) => AttackInput[i] = false;
}

public enum CombatInputs
{
    primary,
    secondary
}
