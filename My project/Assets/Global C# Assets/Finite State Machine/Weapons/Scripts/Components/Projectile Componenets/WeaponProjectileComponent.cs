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
        }

        protected virtual void Start() {
            
        }

        protected virtual void Update() {
            
        }

        protected virtual void FixedUpdate() {
            
        }

        protected virtual void OnDestroy() {
            projectile.OnInIt -= InIt;
        }

        protected virtual void InIt()
        {

        }
    }
}
