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

/// <summary>
/// The EnemySpawnManager class is responsible of managing a event list and spawn enemies on screen.
/// </summary>
public class EnemySpawnManager : MonoBehaviour {

    /// <summary>
    /// The list of events we want to execute.
    /// </summary>
    public EnemySpawnEventList eventList = null;

    /// <summary>
    /// The current event index being executed.
    /// </summary>
    private int eventIndex = 0;

    /// <summary>
    /// The number of repeats for each entry.
    /// </summary>
    private int numberOfRepeats = 0;

    /// <summary>
    /// Spawns an enemy on screen.
    /// </summary>
    /// <param name="spawnEvent">The SpawnEvent instance with all the information to spawn the enemy.</param>
    /// <returns>The enemy GameObject if any. null otherwise.</returns>
    public GameObject SpawnEnemy(EnemySpawnEvent spawnEvent)
    {
        if (spawnEvent.enemyPrefabName != null && spawnEvent.enemyPrefabName.Length > 0)
        {
            GameObject playerShipGameObject = Toolbox.PoolManager.SpawnPrefabNamed(spawnEvent.enemyPrefabName);
            playerShipGameObject.transform.position = spawnEvent.enemyStartPoint;
            EnemyShip enemyShip = playerShipGameObject.GetComponent<EnemyShip>();
            if (enemyShip != null)
            {
                enemyShip.velocity = spawnEvent.enemyVelocity;
                enemyShip.acceleration = spawnEvent.enemyAcceleration;
            }

            return playerShipGameObject;
        }
        return null;
    }

    /// <summary>
    /// Starts executing the events sequentially.
    /// </summary>
    public void StartEvents()
    {
        CancelEvents();
        eventIndex = 0;
        numberOfRepeats = 0;
        Invoke("NextEvent", 0.0f);
    }

    /// <summary>
    /// Cancel the events
    /// </summary>
    public void CancelEvents()
    {
        CancelInvoke("NextEvent");
    }

    /// <summary>
    /// Called in each step of the queue for each event.
    /// </summary>
    private void NextEvent()
    {
        EnemySpawnEventListEntry entry = eventList.entries[eventIndex];
        foreach (EnemySpawnEvent spawnEvent in entry.spawnEvents)
        {
            SpawnEnemy(spawnEvent);
        }
        numberOfRepeats += 1;
        if (numberOfRepeats >= entry.numberOfTimes)
        {
            eventIndex = (eventIndex + 1) % eventList.entries.Length;
            numberOfRepeats = 0;
        }
        Invoke("NextEvent", entry.timeTillNextEvent);
    }

}
