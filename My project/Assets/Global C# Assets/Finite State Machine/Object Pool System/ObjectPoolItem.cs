using UnityEngine;

namespace FoxTail {
    /*
        * Implementation of the object pool item interface that can be used in usual cases
    */
    public class ObjectPoolItem : MonoBehaviour, IObjectPoolItem {
        private ObjectPool objectPool;
        private Component component;

        public void ReturnItem() {
            /*
                * If pool referemve is set then return to the pool
                * Destory else
            */
            if (objectPool != null) objectPool.ReturnObject(component);
            else Destroy(gameObject);
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