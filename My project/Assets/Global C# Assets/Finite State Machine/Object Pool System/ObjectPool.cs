using System.Collections.Generic;
using UnityEngine;

namespace FoxTail {
    public class ObjectPool
    {
        [field: SerializeField] public int StartCount { get; private set; }

        private Dictionary<string, object> pools = new Dictionary<string, object>();

        public ObjectPool<TYPE> GetPool<TYPE>(TYPE prefab) where TYPE : Component
        {
            if (!pools.ContainsKey(prefab.name))
            {
                pools[prefab.name] = new ObjectPool<TYPE>(prefab, StartCount);
            }

            return (ObjectPool<TYPE>)pools[prefab.name];
        }

        public void ReturnObject<TYPE>(TYPE obj) where TYPE : Component
        {
            var objPool = GetPool(obj);
            objPool.ReturnObject(obj);
        }
    }

    public class ObjectPool<TYPE> where TYPE : Component
    {
        private readonly TYPE prefab;
        private readonly Queue<TYPE> poolStack = new Queue<TYPE>();

        public ObjectPool(TYPE prefab, int startCount = 0)
        {
            this.prefab = prefab;

            for (var i = 0; i < startCount; i++)
            {
                var obj = InstantiateNewObject();
                poolStack.Enqueue(obj);
            }
        }

        private TYPE InstantiateNewObject() {
            var obj = Object.Instantiate(prefab);
            obj.name = prefab.name;

            var objectPoolItem = obj.GetComponent<IObjectPoolItem>();
            objectPoolItem.SetObjectPool(this);

            return obj;
        }

        public TYPE GetObject()
        {
            if (!poolStack.TryDequeue(out var obj))
            {
                obj = InstantiateNewObject();
                return obj;
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnObject(TYPE obj)
        {
            obj.gameObject.SetActive(false);
            poolStack.Enqueue(obj);
        }
    }
}