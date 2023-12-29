using System.Collections;
using System.Collections.Generic;
public class Pool <TYPE> where TYPE : new()
{
    private Queue<TYPE> objectPool = new Queue<TYPE>();
    private int rateIncreasePoolSize;

    public Pool(int defaultPoolSize, int rateIncreasePoolSize)
    {
        this.rateIncreasePoolSize = rateIncreasePoolSize;
        AddPool(defaultPoolSize);
    }

    public TYPE GetObject()
    {
        if (objectPool.Count > 0)
        {
            return objectPool.Dequeue();
        }
        else
        {
            AddPool(rateIncreasePoolSize);
            return objectPool.Dequeue();
        }
    }

    public void AddPool(int poolAmount)
    {
        for (int i = 0; i < poolAmount; i++)
        {
            objectPool.Enqueue(new TYPE());
        }
    }

    public void ReturnObject(TYPE poolObject)
    {
        objectPool.Enqueue(poolObject);
    }
}
