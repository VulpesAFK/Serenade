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
        private ObjectPool objectPool;
        private float lastFireTime;
        private ObjectPool<WeaponProjectile> pool;
        private void Awake() {
            objectPool = FindObjectOfType<ObjectPool>();
        }

        private void Start() {
            if (!ProjectilePrefab) {
                Debug.LogWarning("!! No Projectile To Test");
                return;
            }

            pool = objectPool.GetPool(ProjectilePrefab);
            FireProjectile();
        }

        private void FireProjectile() {
            var projectile = pool.GetObject();

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
