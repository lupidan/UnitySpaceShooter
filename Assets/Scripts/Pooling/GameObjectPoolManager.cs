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
using System.Collections;
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
    /// A public list of prefabs that will have it's own prefab GameObjectPool.
    /// </summary>
    public List<GameObject> prefabs = new List<GameObject>();

	void Start ()
    {
        InitializeGameObjectPools();
    }

    /// <summary>
    /// Initializes the GameObject instances pool for all the specified prefabs.
    /// </summary>
    private void InitializeGameObjectPools()
    {
        foreach (GameObject gameObject in prefabs)
        {
            GameObjectPool prefabPool = new GameObjectPool(gameObject, 1);
            gameObjectPools.Add(gameObject.name, prefabPool);
        }
    }

    /// <summary>
    /// Spawns a prefab with a specific name.
    /// </summary>
    /// <param name="name">The name of the profab we want to spawn a instance of.</param>
    /// <returns>The spawned GameObject, or null if there is no prefab pool for that name.</returns>
    public GameObject SpawnPrefabNamed(string name)
    {
        GameObject spawnedObject = null;
        GameObjectPool prefabPool = null;
        if (gameObjectPools.TryGetValue(name, out prefabPool))
        {
            spawnedObject = prefabPool.SpawnObject();
        }
        else
        {
            Debug.LogError("Error: Prefab pool for " + name + " does not exist. Can't spawn Object.");
        }
        return spawnedObject;
    }

    /// <summary>
    /// Recycles a GameObject instance with a specific prefab name. If there is no pool for the specified prefab name, the object is destroyed.
    /// </summary>
    /// <param name="name">The name of the prefab from which the GameObject is an instance of.</param>
    /// <param name="gameObject">The GameObject to recycle in one of the pools.</param>
    public void RecycleGameObject(string name, GameObject gameObject)
    {
        GameObjectPool prefabPool = null;
        if (gameObjectPools.TryGetValue(name, out prefabPool))
        {
            prefabPool.RecycleObject(gameObject);
        }
        else
        {
            Debug.LogError("Error: Prefab pool for " + name + " does not exist. Destroying GameObject");
            Destroy(gameObject);
        }
    }
}
