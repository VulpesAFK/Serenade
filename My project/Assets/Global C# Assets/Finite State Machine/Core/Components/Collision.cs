using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : CoreComponent
{
    // Public settings for global usage
    public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
    public Transform WallCheck { get => wallCheck; private set => wallCheck = value; }
    public Transform LedgeCheck { get => ledgeCheck; private set => ledgeCheck = value; }
    public Transform CeilingCheck { get => ceilingCheck; private set => ceilingCheck = value; }

    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    // Transforms need from the player for surrounding checks
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform ceilingCheck;

    // Check properties
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private LayerMask whatIsGround; 

    // Check the surroundings of the ground
    public bool Ground { get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); }

    // Check the surroundings of the walls in front of us
    public bool WallFront { get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround); }

    // Check the surroundings of the walls behind of us
    public bool WallBack { get => Physics2D.Raycast(wallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround); }

    // Designed to check whether there is a ledge or not
    public bool Ledge { get => Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround); }

    // Designed to check for the ceiling 
    public bool Ceiling { get => Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, whatIsGround); }

}
