using System.Collections.Generic;
using UnityEngine;

namespace FoxTail {
    /*
        * Abstract defination of the class allows use to call the release function without knowing the specific type of object pool responible for  storing the object

        * The object pool class known will also use it in the dictionary 
    */
    public abstract class ObjectPool
    {
        public abstract void Release();
        public abstract void ReturnObject(Component comp);
    }

    /*
        * Generic definition of object pool
        * Generic to allow for any object passed to be defined in the pool of objects

        * Example, we can have many different projectile prefabs with different components attacked to it but the only componenets we are interested in working with when we spawn a projectile is the projectile component attacked to it 
        * Instead of returning the object and called get component everytime a projectile fires we can store it as projectile
    */
    public class ObjectPool<TYPE> : ObjectPool where TYPE : Component
    {
        /*
            * Created pool with a passed prefab that should be used in the new components

            * The prefabs can be stored as any of the components attached to it and does not need to be stored as a game object variable 
        */
        private readonly TYPE prefab;

        // * The inactive objects returned when one is requested
        private readonly Queue<TYPE> pool = new Queue<TYPE>();

        /*
            * All objects that are part of this pool
            * Inactive and active if they have a component that implements the object pool item interface 
        */
        private readonly List<IObjectPoolItem> allItems = new List<IObjectPoolItem>();

        // * Constructor defines the prefab and initializes some components 
        public ObjectPool(TYPE prefab, int startCount = 1)
        {
            this.prefab = prefab;

            for (var i = 0; i < startCount; i++)
            {
                var obj = InstantiateNewObject();
                pool.Enqueue(obj);
            }
        }

        // * Instantiates a new component when needed
        private TYPE InstantiateNewObject() {
            var obj = Object.Instantiate(prefab);
            obj.name = prefab.name;

            if (!obj.TryGetComponent<IObjectPoolItem>(out var objectPoolItem)) {
                Debug.LogWarning($"{obj.name} does not have a component that implements IObjectPoolItem");
                return obj;
            }

            // * If it contains the object pool interface then set this object pool as its pool and store in list
            objectPoolItem.SetObjectPool(this, obj);
            allItems.Add(objectPoolItem);
            
            return obj;
        }

        public TYPE GetObject()
        {
            /*
                * Try to get an item from the queue

                * Try dequeue returns the object if object available
                * Return false if there is not
            */
            if (!pool.TryDequeue(out var obj))
            {
                // * Instantiate a new one and return
                obj = InstantiateNewObject();
                return obj;
            }

            // * If available then return
            obj.gameObject.SetActive(true);
            return obj;
        }

        /*
            * Return object to the queue
            * Usually called from the component that implements the object pool item interface
        */
        public override void ReturnObject(Component comp)
        {
            if (comp is not TYPE compObj) {
                return;
            }

            compObj.gameObject.SetActive(false);
            pool.Enqueue(compObj);
        }

        /*
            * Called when the pool is no longer needed
            * Destroys all inactive objects and releases the active ones
            * Released objects should destroy self when attempting to return to the pool
        */
        public override void Release()
        {
            foreach (var item in pool)
            {
                allItems.Remove(item as IObjectPoolItem);
                Object.Destroy(item.gameObject);
            }

            foreach (var item in allItems)
            {
                item.Release();
            }
        }
    }
}