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
using UnityEngine.Assertions;
using System.Collections.Generic;

/// <summary>
/// A GameObjectPoolManager is able to manage several GameObjectPool instances for the input prefabs.
/// </summary>
public class GameObjectPoolManager : MonoBehaviour {

    /// <summary>
    /// Private dictionary of GameObjectPool indexed by the prefab name of each pool's GameObject.
    /// </summary>
    private Dictionary<string, GameObjectPool> gameObjectPools = new Dictionary<string, GameObjectPool>();

    /// <summary>
    /// Returns a Game Object Pool for a specific GameObject. If no Game Object Pool for that prefab exists, we create one.
    /// </summary>
    /// <param name="gameObject">The GameObject we want to retrieve the Game Object Pool of. It can't be null.</param>
    /// <returns>The GameObject pool, or null if something went wrong.</returns>
    public GameObjectPool GameObjectPoolForPrefab(GameObject gameObject)
    {
        Assert.IsNotNull(gameObject);
        Assert.IsNotNull(gameObject.name);

        string gameObjectPoolKey = gameObject.name;
        GameObjectPool gameObjectPool = null;
        if (!gameObjectPools.TryGetValue(gameObjectPoolKey, out gameObjectPool))
        {
            gameObjectPool = new GameObjectPool(gameObject, 1);
            gameObjectPools.Add(gameObjectPoolKey, gameObjectPool);
        }
        return gameObjectPool;
    }

    /// <summary>
    /// Spawns a game object using the pooling method.
    /// </summary>
    /// <param name="gameObject">The GameObject we want to spawn a instance of.</param>
    /// <returns>The spawned GameObject, or null if there is no game object pool for that name.</returns>
    public GameObject SpawnGameObject(GameObject gameObject)
    {
        GameObjectPool gameObjectPool = GameObjectPoolForPrefab(gameObject);
        return gameObjectPool.SpawnObject();
    }

    /// <summary>
    /// Recycles a GameObject instance with a specific name. If there is no pool for the specified game object name, the object is destroyed.
    /// </summary>
    /// <param name="gameObject">The GameObject to recycle in one of the pools.</param>
    public void RecycleGameObject(GameObject gameObject)
    {
        GameObjectPool gameObjectPool = null;
        if (gameObjectPools.TryGetValue(gameObject.name, out gameObjectPool))
        {
            gameObjectPool.RecycleObject(gameObject);
        }
        else
        {
            Debug.LogError("Error: Game Object pool for " + name + " does not exist. Destroying GameObject");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Recycle all spawned objects on screen.
    /// </summary>
    public void RecycleAllSpawnedObjects()
    {
        foreach (GameObjectPool gameObjectPool in gameObjectPools.Values)
        {
            gameObjectPool.RecycleAllObjects();
        }
    }

    void Start()
    {
        
    }

}
