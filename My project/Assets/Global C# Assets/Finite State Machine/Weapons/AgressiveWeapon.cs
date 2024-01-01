using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AgressiveWeapon : Weapon
{
    private List<IDamageable> detectedDamageable = new List<IDamageable>();

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
    }

    public void AddToDetected(Collider2D other)
    {
        Debug.Log("Added");
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null) { detectedDamageable.Add(damageable); }
    }
    public void RemoveFromDetected(Collider2D other)
    {
        Debug.Log("Gone");
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null) { detectedDamageable.Remove(damageable); }
    }  
}
