using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FoxTail {
    /*
        * A tester development weapon system we need to develop and test
        * Allows for it to be more easier to test to make sure everything is working 
    */
    public class ProjectileTester : MonoBehaviour {
        public WeaponProjectile ProjectilePrefab;
        public DamageDataPackage DamageDataPackage;

        public float ShotCooldown;
        private ObjectPool objectPool = new ObjectPool();
        private float lastFireTime;

        private void Start() {
            if (!ProjectilePrefab) {
                Debug.LogWarning("!! No Projectile To Test");
                return;
            }

            FireProjectile();
        }

        private void FireProjectile() {
            var projectile = objectPool.GetPool(ProjectilePrefab).GetObject();

            projectile.Reset();

            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;
            ProjectilePrefab.SendDataPackage(DamageDataPackage);

            ProjectilePrefab.InIt();

            lastFireTime = Time.time;
        }

        private void Update() {
            if (Time.time >= lastFireTime + ShotCooldown)
            {
                FireProjectile();
            }
        }
    }
}
