using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public bool CanSetVelocity { get; set; }
    public int FacingDirection { get; private set; }
    private Vector2 workSpace;

    protected override void Awake() {
        base.Awake();

        RB = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;

        CanSetVelocity = true;
    }

    public override void LogicUpdate() => CurrentVelocity = RB.velocity;

    public void SetVelocityX(float velocity) {
        workSpace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity) {
        workSpace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction) {
        angle.Normalize();
        workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 direction) {
        workSpace = direction * velocity;
        SetFinalVelocity();
    }

    public void SetVelocityZero() {
        workSpace = Vector2.zero;
        SetFinalVelocity();
    }

    private void SetFinalVelocity() {
        if (CanSetVelocity) {
            RB.velocity = workSpace;
            CurrentVelocity = workSpace;
        }
    }

    public void CheckIfShouldFlip(int xInput) {
        if (xInput != 0 && xInput != FacingDirection) { Flip(); }
    }
    
    public void Flip() {
        FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
