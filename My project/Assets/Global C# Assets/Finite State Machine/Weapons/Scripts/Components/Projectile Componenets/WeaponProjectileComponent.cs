using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class WeaponProjectileComponent : MonoBehaviour
    {
        protected WeaponProjectile projectile;
        protected Rigidbody2D rb => projectile.Rigidbody2D;
        public bool Active { get; private set; }

        protected virtual void Awake() {
            projectile = GetComponent<WeaponProjectile>();
            projectile.OnInIt += InIt;
            projectile.OnReset += Reset;
            projectile.OnReceiveDataPackage += HandleReceiveDataPackage;
        }

        public virtual void SetActive(bool value) => Active = value;

        public virtual void SetActiveNextFrame(bool value) {
            StartCoroutine(SetActiveNextFrameCoroutine(value));
        }

        public IEnumerator SetActiveNextFrameCoroutine(bool value)
        {
            yield return null;
            SetActive(value);
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
            SetActive(true);
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
