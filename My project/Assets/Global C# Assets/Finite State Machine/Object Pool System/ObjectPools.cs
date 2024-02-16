using System;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail {
    /*
        * A multi class used to store multiple object pools in a single place with an easy method to access them
        * Stores a dictionary where the key is the name of the prefab associated with that pool
        * If there is an attempt to access an object from another pool or from a pool that does not exist in contect a new pool is created for that prefab
    */
    public class ObjectPools {
        private readonly Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();
        public ObjectPool<TYPE> GetPool<TYPE>(TYPE prefab, int startCount = 1) where TYPE : Component
        {
            if (!pools.ContainsKey(prefab.name))
            {
                pools[prefab.name] = new ObjectPool<TYPE>(prefab, startCount);
            }

            return (ObjectPool<TYPE>)pools[prefab.name];
        }
        
        public void ReturnObject<TYPE>(TYPE obj) where TYPE : Component {
            var objPool = GetPool(obj);
            objPool.ReturnObject(obj);
        }

        public void Release() {
            foreach (var pool in pools)
            {
                pool.Value.Release();
            }
        }
    }
}