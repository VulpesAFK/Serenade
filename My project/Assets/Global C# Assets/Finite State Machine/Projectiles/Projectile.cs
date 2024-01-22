using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //private AttackDetails attackDetails;
    private float speed;
    private float travelDistance;
    private float xStartPosition;
    private Rigidbody2D RB;
    private bool isGravityOn;
    private bool hasHitGround;

    [SerializeField] private float gravity;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform damagePosition;
    [SerializeField] private float damageRadius;

    private void Start() {
        RB = GetComponent<Rigidbody2D>();

        RB.gravityScale = 0;
        RB.velocity = transform.right * speed;

        xStartPosition = transform.position.x;

        isGravityOn = false;
    }

    private void Update() {
        if(!hasHitGround)
        {
            //attackDetails.Position = transform.position;

            if(isGravityOn)
            {
                float angle = Mathf.Atan2(RB.velocity.y, RB.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate() {
        if(!hasHitGround) 
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if(damageHit)
            {
                //damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }

            if(groundHit)
            {
                hasHitGround = true;
                RB.gravityScale = 0;
                RB.velocity = Vector2.zero;
            }

            if (Mathf.Abs(xStartPosition - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                RB.gravityScale = gravity;
            }
        }
        
    }

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        //attackDetails.DamageAmount = damage;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
