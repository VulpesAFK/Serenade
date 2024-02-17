using System.Collections;
using UnityEngine;

namespace FoxTail {
    /*
        * Implementation of the object pool item interface that can be used in usual cases
    */
    public class ObjectPoolItem : MonoBehaviour, IObjectPoolItem {
        private ObjectPool objectPool;
        private Component component;

        public void ReturnItem(float delay = 0f) {
            if (delay > 0)
            {
                StartCoroutine(ReturnItemWithDelay(delay));
                return;
            }

            ReturnItemToPool();
        }

        public void ReturnItemToPool() {
            /*
                * If pool referemve is set then return to the pool
                * Destory else
            */
            if (objectPool != null) objectPool.ReturnObject(component);
            else Destroy(gameObject);
        }

        private IEnumerator ReturnItemWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            ReturnItemToPool();
        }

        public void SetObjectPool<TYPE>(ObjectPool pool, TYPE comp) where TYPE : Component {
            objectPool = pool;
            component = GetComponent(comp.GetType());
        }

        public void Release() {
            objectPool = null;
        }
    }
}