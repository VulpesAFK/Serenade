using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    private Vector2 workSpace;


    protected override void Awake()
    {
        base.Awake();

        // Fetch all necessary components required to function accordingly
        RB = GetComponentInParent<Rigidbody2D>();

        // Set the default direction to right 
        FacingDirection = 1;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    // Functions for all notion to player movement 
    #region Velocity functions to alter the player position

        # region SetVelocityX
    // Function to help aid any state to force movement via the x axis
    public void SetVelocityX(float velocity)
    {
        // Set the workspace velocity with an arguement and the current velocity y
        workSpace.Set(velocity, CurrentVelocity.y);
        // Set the rigidbody2d with the set vector2 
        RB.velocity = workSpace;
        //  Set the control vector2 with the forced vector2 
        CurrentVelocity = workSpace;
    }

        # endregion

        # region SetVelocityY
    // Function to help aid any state to force movement via the y axis
    public void SetVelocityY(float velocity)
    {
        // Set the workspace velocity with san arugement and the current velcoity x
        workSpace.Set(CurrentVelocity.x, velocity);
        // Set the rigidbody2d with the set vector2
        RB.velocity = workSpace;
        // Set the control vector2 with the forced vector2
        CurrentVelocity = workSpace;
    }

        # endregion

        # region SetVelocity
    // Function to force the movement of the player with a force under a specific direction
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        // Set the angle vector2 arguement to be in a fixed magitude of one
        angle.Normalize();
        // Set the vector2 force to the with the of the angle and velocity under a specific x direction
        workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        // Set the rigidbody with the new work vector2
        RB.velocity = workSpace;
        // Change the current vector2 force to match with the added
        CurrentVelocity = workSpace;
    }

        # endregion

        # region SetVelocity
    // Another function that will support the velocity force with only a vector2 and float
    public void SetVelocity(float velocity, Vector2 direction)
    {
        // Set the workspace with a specific direction and velocity
        workSpace = direction * velocity;
        // Set the workspace to the actual rigidbody2d
        RB.velocity = workSpace;
        // Correct the current velocity to the new updated added force
        CurrentVelocity = workSpace;
    }

        # endregion

        # region SetVelocityZero
    // Freeze the player two its current position flat
    public void SetVelocityZero()
    {
        // Freeze the rigidbody2d at its place
        RB.velocity = Vector2.zero;
        // Update the current velocity to these changes
        CurrentVelocity = Vector2.zero;
    }

        # endregion

    # endregion

    // Flips the player via rotation or can be scale
    private void Flip()
    {
        // Alters the facing direction to match the current
        FacingDirection *= -1;
        // Rotation via the y axis
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    // Checks if there should be a sprite flip
    public void CheckIfShouldFlip(int xInput)
    {
        // Checks if the input pressed is different to that of the variable holding the old direction
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

}