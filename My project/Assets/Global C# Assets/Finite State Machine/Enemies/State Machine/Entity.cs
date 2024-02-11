using UnityEngine;

public class Entity : MonoBehaviour
{
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyEntityData EntityData;
    public Animator Anim { get; private set; }
    public AnimationToStateMachine AnimationToStateMachine { get; private set; }
    public Core Core { get; private set; }

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

    protected Stats stats;

    public int lastDamageDirection { get; private set; }

    private Movement Movement { get => movement ??= Core.GetCoreComponent<Movement>(); }
    private Movement movement;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        Anim = GetComponent<Animator>(); 
        AnimationToStateMachine = GetComponent<AnimationToStateMachine>();

        StateMachine = new EnemyStateMachine();

        currentHealth = EntityData.MaxHealth;
        currentStunResistance = EntityData.StunResistance;
        
        stats = Core.GetCoreComponent<Stats>();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();

        Anim.SetFloat("yVelocity", Movement.RB.velocity.y);

        if (Time.time >= lastDamageTime + EntityData.StunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, EntityData.MinAggroDistance, EntityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, EntityData.MaxAggroDistance, EntityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, EntityData.CloseRangeActionDistance, EntityData.whatIsPlayer);
    }



    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = EntityData.StunResistance;
    }

    public virtual void DamageHop(float velocity)
    {

        workSpace.Set(Movement.RB.velocity.x, velocity);
        Movement.RB.velocity = workSpace;

    }   

    public virtual void OnDrawGizmos() {

        if(Core != null) {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Movement?.FacingDirection * EntityData.WallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * EntityData.LedgeCheckDistance));

            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * Movement?.FacingDirection * EntityData.CloseRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * Movement?.FacingDirection * EntityData.MinAggroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * Movement?.FacingDirection * EntityData.MaxAggroDistance), 0.2f);
        }
    }

}
