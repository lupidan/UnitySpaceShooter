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

public class EnemySpawnManager : MonoBehaviour {

    /// <summary>
    /// The pool manager to use to Spawn GameObjects
    /// </summary>
    public GameObjectPoolManager poolManager = null;

    /// <summary>
    /// The list of enemy ship spawn points
    /// </summary>
    public List<EnemySpawnPoint> spawnPointList = new List<EnemySpawnPoint>();

    /// <summary>
    /// Dictionary containing all the spawn points indexed by name
    /// </summary>
    private Dictionary<string, EnemySpawnPoint> spawnPointDictionary = new Dictionary<string, EnemySpawnPoint>();

    /// <summary>
    /// Populates the position dictionary from the input spawn position list.
    /// </summary>
    private void PopulateSpawnPositionsDictionary()
    {
        foreach (EnemySpawnPoint spawnPoint in spawnPointList)
        {
            spawnPointDictionary.Add(spawnPoint.name, spawnPoint);
        }
    }

    /// <summary>
    /// Spawn a enemy according to the data from a EnemySpawnPoint object
    /// </summary>
    /// <param name="enemySpawnPoint">The EnemySpawnPoint containing the information.</param>
    public void SpawnEnemy(EnemySpawnPoint enemySpawnPoint, string enemyPrefabName)
    {
        GameObject playerShipGameObject = poolManager.SpawnPrefabNamed(enemyPrefabName);
        Vector3 position = transform.position;
        position.x = Random.Range(enemySpawnPoint.spawnArea.xMin, enemySpawnPoint.spawnArea.xMax);
        position.y = Random.Range(enemySpawnPoint.spawnArea.yMin, enemySpawnPoint.spawnArea.yMax);
        playerShipGameObject.transform.position = position;

        EnemyShip enemyShip = playerShipGameObject.GetComponent<EnemyShip>();
        if (enemyShip != null)
        {
            enemyShip.velocity = enemySpawnPoint.shipVelocity;
            enemyShip.acceleration = enemySpawnPoint.shipAcceleration;
            enemyShip.poolManager = poolManager;
        }
    }

    void Start ()
    {
        PopulateSpawnPositionsDictionary();
    }


}
