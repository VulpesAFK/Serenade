using System.Collections;
using UnityEngine;

namespace FoxTail {
    public class ReturnToPoolTester : WeaponProjectileComponent {
        private ObjectPoolItem objectPoolItem;

        protected override void Awake()
        {
            base.Awake();

            objectPoolItem = GetComponent<ObjectPoolItem>();
        }
        protected override void InIt()
        {
            base.InIt();

            // * Return to the pool after a X amount of time 
            StartCoroutine(ReturnToPool());
        }

        private IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(1f);
            objectPoolItem.ReturnItem();
        }
    }
}