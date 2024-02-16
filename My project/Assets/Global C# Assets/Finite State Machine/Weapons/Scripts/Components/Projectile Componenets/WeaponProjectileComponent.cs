using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponProjectileComponent : MonoBehaviour
    {
        protected WeaponProjectile projectile;

        protected Rigidbody2D rb => projectile.Rigidbody2D;

        protected virtual void Awake() {
            projectile = GetComponent<WeaponProjectile>();
            projectile.OnInIt += InIt;
            projectile.OnReset += Reset;
            projectile.OnReceiveDataPackage += HandleReceiveDataPackage;
        }

        protected virtual void Start() {
            
        }

        protected virtual void Update() {
            
        }

        protected virtual void FixedUpdate() {
            
        }

        protected virtual void OnDestroy() {
            projectile.OnInIt -= InIt;
            projectile.OnReset -= Reset;
            projectile.OnReceiveDataPackage -= HandleReceiveDataPackage;
        }

        protected virtual void InIt()
        {

        }

        protected virtual void Reset()
        {

        }

        /* 
            * Handles receiving specific data packages from the weapon 
            * Implemented in any component that needs packages and automatically subscribes then to it 
        */
        protected virtual void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {

        }
    }
}
