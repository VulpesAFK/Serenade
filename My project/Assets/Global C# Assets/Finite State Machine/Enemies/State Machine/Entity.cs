using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyEntityData EntityData;
    public int FacingDirection { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }
    public GameObject AliveGO { get; private set; }
    public AnimationToStateMachine AnimationToStateMachine { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform groundCheck;
    private Vector2 workSpace;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    protected bool isStunned;
    protected bool isDead;

    public int lastDamageDirection { get; private set; }


    public virtual void Start()
    {
        AliveGO = transform.Find("Alive").gameObject;
        RB = AliveGO.GetComponent<Rigidbody2D>();
        Anim = AliveGO.GetComponent<Animator>(); 
        AnimationToStateMachine = AliveGO.GetComponent<AnimationToStateMachine>();

        FacingDirection = 1;

        StateMachine = new EnemyStateMachine();

        currentHealth = EntityData.MaxHealth;
        currentStunResistance = EntityData.StunResistance;
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();

        if (Time.time >= lastDamageTime + EntityData.StunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {   
        workSpace.Set(FacingDirection * velocity, RB.velocity.y);
        RB.velocity = workSpace;

    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, AliveGO.transform.right, EntityData.WallCheckDistance, EntityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, EntityData.LedgeCheckDistance, EntityData.whatIsGround);
    }

    public virtual void Flip()
    {
        FacingDirection *= -1;
        AliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGO.transform.right, EntityData.MinAggroDistance, EntityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGO.transform.right, EntityData.MaxAggroDistance, EntityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGO.transform.right, EntityData.CloseRangeActionDistance, EntityData.whatIsPlayer);
    }
    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, EntityData.groundCheckRadius, EntityData.whatIsGround);
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
         angle.Normalize();
         workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
         RB.velocity = workSpace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = EntityData.StunResistance;
    }

    public virtual void DamageHop(float velocity)
    {

        workSpace.Set(RB.velocity.x, velocity);
        RB.velocity = workSpace;

    }   

    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;

        currentHealth -= attackDetails.DamageAmount;
        currentStunResistance -= attackDetails.StunDamageAmount;

        DamageHop(EntityData.DamageHopVelocity);

        Instantiate(EntityData.HitParticle, AliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if(attackDetails.Position.x > AliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public virtual void OnDrawGizmos() {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * FacingDirection * EntityData.WallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * EntityData.LedgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * FacingDirection * EntityData.CloseRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * FacingDirection * EntityData.MinAggroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * FacingDirection * EntityData.MaxAggroDistance), 0.2f);
    }

}
