using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AgressiveWeapon : Weapon
{
    private List<IDamageable> detectedDamageable = new List<IDamageable>();
    private List<IKnockbackable> detectedKnockable = new List<IKnockbackable>();
    protected AgressiveWeaponData agressiveWeaponData;
    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;
    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(AgressiveWeaponData))
        {
            agressiveWeaponData = (AgressiveWeaponData) weaponData;
        }
        else 
        {
            Debug.LogError("Wrong Data for the weapon");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        WeaponStruct details = agressiveWeaponData.WeaponDetails[attackCounter];

        foreach (IDamageable item in detectedDamageable.ToList())
        {   
            item.Damage(details.DamageAmount);
        }

        foreach (IKnockbackable item in detectedKnockable.ToList())
        {   
            item.Knockback(details.KnockbackAngle, details.KnockbackStrength, Movement.FacingDirection);
        }
    }

    public void AddToDetected(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null) { detectedDamageable.Add(damageable); }

        IKnockbackable knockbackable = other.GetComponent<IKnockbackable>();
        if (knockbackable != null) { detectedKnockable.Add(knockbackable); }
    }
    public void RemoveFromDetected(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null) { detectedDamageable.Remove(damageable); }

        IKnockbackable knockbackable = other.GetComponent<IKnockbackable>();
        if (knockbackable != null) { detectedKnockable.Remove(knockbackable); }
    }  
}
