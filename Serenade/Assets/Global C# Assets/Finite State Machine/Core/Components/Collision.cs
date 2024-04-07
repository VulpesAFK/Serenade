using UnityEngine;

public class Collision : CoreComponent {
    // Public settings for global usage
    public Transform GroundCheck { 
        get => GenericNonImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value; 
    }
    public Transform WallCheck { 
        get => GenericNonImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private set => wallCheck = value; 
    }
    public Transform LedgeCheckHorizontal { 
        get => GenericNonImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
        private set => ledgeCheckHorizontal = value; 
    }
    public Transform LedgeCheckVertical { 
        get => GenericNonImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
        private set => ledgeCheckVertical = value; 
    }
    public Transform CeilingCheck { 
        get => GenericNonImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
        private set => ceilingCheck = value;
    }
    public Transform InteractCheck { 
        get => GenericNonImplementedError<Transform>.TryGet(interactCheck, core.transform.parent.name);
        private set => interactCheck = value;
    }

    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public float InteractCheckDistance { get => interactiveCheckRadius; set => interactiveCheckRadius = value; }



    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }
    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;


    // Transforms need from the player for surrounding checks
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform interactCheck;

    // Check properties
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private float interactiveCheckRadius = 0.7f;
    [SerializeField] private LayerMask whatIsGround; 

    // Check the surroundings of the ground
    public bool Ground { get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround); }

    // Check the surroundings of the walls in front of us
    public bool WallFront { get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround); }

    // Check the surroundings of the walls behind of us
    public bool WallBack { get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, wallCheckDistance, whatIsGround); }

    // Designed to check whether there is a ledge or not
    public bool LedgeHorizontal { get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround); }
    public bool LedgeVertical { get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround); }
    // Designed to check for the ceiling 
    public bool Ceiling { get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround); }

}
