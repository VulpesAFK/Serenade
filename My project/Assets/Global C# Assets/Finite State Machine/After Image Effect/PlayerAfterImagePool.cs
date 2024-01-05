using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    // Holds the gameobject that will be instantiated when used
    [SerializeField] private GameObject afterImagePrefab;
    // Normal instantiated size
    [SerializeField] private int defaultPoolSize = 10;
    // Rate of increase
    [SerializeField] private int rateIncreasePoolSize = 5;
    // Max pool size threshold
    [SerializeField] private int maxPoolSizeOverFlow = 25;

    // A new queue set to hold all the total objects that will be fetched
    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    // A variable that will hold the instanance reference to this script
    public static PlayerAfterImagePool Instance { get; private set; }

    // Variables set to make a clear instantiation of the pool and reference settings
    private void Awake() 
    {
        // Assign a encapsulated reference to the script
        Instance = this;
        // Grow the pool to make sure there are objects to play with
        GrowPool(defaultPoolSize);
    }

    // Grow the pool of objects
    private void GrowPool(float growthAmount)
    {
        // Loop through the amount of objects wanted
        for (int i = 0; i < growthAmount; i++)
        {
            // Assign the prefab to the a guessed variable formatter
            var instanceToAdd = Instantiate(afterImagePrefab);
            // Set the new transform of the child to the parent
            instanceToAdd.transform.SetParent(transform);

            // Add to the pool
            AddToPool(instanceToAdd);
        }
    }

    // Add to the pool
    public void AddToPool(GameObject instance)
    {
        // Set active to be on standby
        instance.SetActive(false);
        // Queue the object up in line
        availableObjects.Enqueue(instance);
    }  

    // Fetching from the pool
    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0 && transform.childCount < maxPoolSizeOverFlow)
        {
            // Grow the queue length even further
            GrowPool(rateIncreasePoolSize);
        }

        // Remove from the queue
        var instance = availableObjects.Dequeue();
        // Set active for usage
        instance.SetActive(true);

        return instance;
    } 

}
