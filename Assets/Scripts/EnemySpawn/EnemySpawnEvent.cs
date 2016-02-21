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
/// The EnemySpawnEvent defines an event managed by the EnemySpawnManager event queue.
/// </summary>
[System.Serializable]
public class EnemySpawnEvent
{
    /// <summary>
    /// The name of the enemy ship prefab to instantiate.
    /// </summary>
    public string enemyPrefabName = "EnemyShip_";

    /// <summary>
    /// The starting position of the enemy when spawned.
    /// </summary>
    public Vector3 enemyStartPoint = Vector3.zero;

    /// <summary>
    /// The starting velocity of the enemy when spawned.
    /// </summary>
    public Vector3 enemyVelocity = Vector3.zero;

    /// <summary>
    /// The acceleration of the enemy when spawned.
    /// </summary>
    public Vector3 enemyAcceleration = Vector3.zero;
}
