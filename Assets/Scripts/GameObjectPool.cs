using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// The IPooledObject interface defines an object that can be pooled by a ObjectPool instance.
/// </summary>
public interface IPooledObject
{
    /// <summary>
    /// Method called by the pool when a object from it is created or reused.
    /// </summary>
    void OnSpawn();

    /// <summary>
    /// Method called by the pool when a object from it is recycled.
    /// </summary>
    void OnDespawn();
}

/// <summary>
/// The GameObjectPool class implements a pool of GameObjects.
/// A GameObjectPool should only pool one type of GameObject
/// The cost of creating instances of GameObject is big.
/// This pool allows to retrieve and recycle GameObjects instead of instantiating a new one everytime one is needed.
/// </summary>
public class GameObjectPool {

    /// <summary>
    /// The private pool of game objects that can be reused again.
    /// </summary>
    private Queue<GameObject> pooledObjects;

    /// <summary>
    /// The Prefab GameObject that we want this pool to work
    /// </summary>
    public GameObject pooledPrefab { get; private set; }

    /// <summary>
    /// Creates a GameObjectPool for a specific GameObject. With an optional initial amount.
    /// </summary>
    /// <param name="gameObject">The GameObject this pool will instantiate.</param>
    /// <param name="initialSize">The initial amount of instances available in this pool.</param>
    public GameObjectPool(GameObject gameObject, int initialSize = 0)
    {
        pooledObjects = new Queue<GameObject>();
        pooledPrefab = gameObject;
        if (initialSize > 0)
        {
            InstantiatePooledObjects(initialSize);
        }
    }

    /// <summary>
    /// Spawns an object from the pool queue. If the GameObject contains Components implementing the IPooledObject interface, OnSpawn is called for those components.
    /// </summary>
    /// <returns>The reused GameObject.</returns>
    public GameObject SpawnObject()
    {
        GameObject gameObject = null;
        if (pooledObjects.Count == 0)
        {
            InstantiatePooledObject();
        }
        gameObject = pooledObjects.Dequeue();
        gameObject.SetActive(true);
        gameObject.name = pooledPrefab.name;

        IPooledObject[] pooledObjectComponents = gameObject.GetComponents<IPooledObject>();
        foreach (IPooledObject pooledObjectComponent in pooledObjectComponents)
        {
            pooledObjectComponent.OnSpawn();
        }
        return gameObject;
    }

    /// <summary>
    /// Recycles a GameObject into the specified pool. If the GameObject contains Components implementing the IPooledObject interface, OnDespawn is called for those components.
    /// </summary>
    /// <param name="gameObject">The GameObject to recycle.</param>
    public void RecycleObject(GameObject gameObject)
    {
        pooledObjects.Enqueue(gameObject);
        gameObject.SetActive(false);

        IPooledObject[] pooledObjectComponents = gameObject.GetComponents<IPooledObject>();
        foreach (IPooledObject pooledObjectComponent in pooledObjectComponents)
        {
            pooledObjectComponent.OnDespawn();
        }
    }

    /// <summary>
    /// Instantiates a pooled object and adds it to the queue.
    /// </summary>
    /// <returns>The instantiated GameObject that was added to the pool GameObject queue.</returns>
    private GameObject InstantiatePooledObject()
    {
        GameObject gameObject = GameObject.Instantiate(pooledPrefab);
        pooledObjects.Enqueue(gameObject);
        gameObject.SetActive(false);
        return gameObject;
    }

    /// <summary>
    /// Instantiates a number of pooled objects, and add those to the queue.
    /// </summary>
    /// <param name="numberOfObjects">The number of objects to instantiate and add to the pool GameObject queue.</param>
    private void InstantiatePooledObjects(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i += 1)
        {
            InstantiatePooledObject();
        }
    }
}
