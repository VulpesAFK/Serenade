using System.Collections;
using UnityEngine;

namespace FoxTail {
    public class ReturnToPoolTester : WeaponProjectileComponent, IObjectPoolItem {
        private ObjectPool<WeaponProjectile> objectPool;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void InIt()
        {
            base.InIt();

            StartCoroutine(ReturnToPool());
        }

        private IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(1f);
            objectPool.ReturnObject(projectile);
        }

        public void SetObjectPool<T>(ObjectPool<T> pool) where T : Component
        {
            objectPool = pool;
        }
    }
}