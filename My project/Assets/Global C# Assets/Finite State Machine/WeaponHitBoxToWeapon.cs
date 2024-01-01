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
        Debug.Log("On trigger we hit");
        agressiveWeapon.AddToDetected(other);
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Exit trigger we leave");
        agressiveWeapon.RemoveFromDetected(other);
    }
}
