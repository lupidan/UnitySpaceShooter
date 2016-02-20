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

	// Use this for initialization
	void Start () {
        InitializeGameObjectPools();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void InitializeGameObjectPools()
    {
        foreach (GameObject gameObject in prefabs)
        {
            GameObjectPool prefabPool = new GameObjectPool(gameObject, 10);
            gameObjectPools.Add(gameObject.name, prefabPool);
        }
    }

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
