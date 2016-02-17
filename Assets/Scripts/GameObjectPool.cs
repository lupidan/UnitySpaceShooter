using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPooledObject
{
    void OnSpawn();
    void OnDespawn();
}

public class ObjectPool {

    private Queue<GameObject> pooledObjects;

    public GameObject pooledPrefab { get; private set; }
    
    public ObjectPool(GameObject gameObject)
    {
        pooledObjects = new Queue<GameObject>();
        pooledPrefab = gameObject;
    }

    public GameObject SpawnObject()
    {
        GameObject gameObject = null;
        if (pooledObjects.Count > 0)
        {
            gameObject = pooledObjects.Dequeue();
        }
        else
        {
            gameObject = GameObject.Instantiate(pooledPrefab);
        }
        gameObject.SetActive(true);

        IPooledObject[] pooledObjectComponents = gameObject.GetComponents(typeof(IPooledObject)) as IPooledObject[];
        foreach (IPooledObject pooledObjectComponent in pooledObjectComponents)
        {
            pooledObjectComponent.OnSpawn();
        }

        return gameObject;
    }

    public bool RecycleObject(GameObject gameObject)
    {
        pooledObjects.Enqueue(gameObject);
        gameObject.SetActive(false);

        IPooledObject[] pooledObjectComponents = gameObject.GetComponents(typeof(IPooledObject)) as IPooledObject[];
        foreach (IPooledObject pooledObjectComponent in pooledObjectComponents)
        {
            pooledObjectComponent.OnSpawn();
        }

        return true;
    }
}
