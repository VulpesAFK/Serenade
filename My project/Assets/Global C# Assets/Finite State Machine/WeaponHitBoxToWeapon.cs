using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitBoxToWeapon : MonoBehaviour
{
    private AgressiveWeapon agressiveWeapon;

    private void Awake() {
        agressiveWeapon =  GetComponentInParent<AgressiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        agressiveWeapon.AddToDetected(other);
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        agressiveWeapon.RemoveFromDetected(other);
    }
}
