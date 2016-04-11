///
/// The MIT License(MIT)
/// 
/// Copyright(c) 2016 Daniel Lupiañez Casares
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.
///

using UnityEngine;
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
    /// The list of spawned objects
    /// </summary>
    private List<GameObject> spawnedObjects;

    /// <summary>
    /// The Prefab GameObject that we want this pool to work
    /// </summary>
    public GameObject pooledPrefab { get; private set; }

    /// <summary>
    /// The last number of created instances. This will be used to increase the number of pooled objects available exponentially.
    /// </summary>
    private int lastCreatedAmount = 0;

    /// <summary>
    /// Creates a GameObjectPool for a specific GameObject. With an optional initial amount.
    /// </summary>
    /// <param name="gameObject">The GameObject this pool will instantiate.</param>
    /// <param name="initialSize">The initial amount of instances available in this pool.</param>
    public GameObjectPool(GameObject gameObject, int initialSize = 0)
    {
        pooledObjects = new Queue<GameObject>();
        spawnedObjects = new List<GameObject>();
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
            InstantiatePooledObjects(lastCreatedAmount * 2);
        }
        gameObject = pooledObjects.Dequeue();
        gameObject.SetActive(true);
        gameObject.name = pooledPrefab.name;
        gameObject.transform.parent = null;

        spawnedObjects.Add(gameObject);

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

        spawnedObjects.Remove(gameObject);

        IPooledObject[] pooledObjectComponents = gameObject.GetComponents<IPooledObject>();
        foreach (IPooledObject pooledObjectComponent in pooledObjectComponents)
        {
            pooledObjectComponent.OnDespawn();
        }
    }

    /// <summary>
    /// Recycle all the spawned objects.
    /// </summary>
    public void RecycleAllObjects()
    {
        GameObject[] objectsToRecycle = spawnedObjects.ToArray();
        foreach (GameObject objectToRecycle in objectsToRecycle)
        {
            RecycleObject(objectToRecycle);
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
        lastCreatedAmount = numberOfObjects;
    }
}
