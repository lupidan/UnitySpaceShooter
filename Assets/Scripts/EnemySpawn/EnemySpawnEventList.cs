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
/// The EnemySpawnEventList defines a list of EnemySpawnEvent objects that can be edited in an asset to define levels.
/// </summary>
[CreateAssetMenu(fileName = "EventList", menuName = "Spawn Event List")]
public class EnemySpawnEventList : ScriptableObject
{
    /// <summary>
    /// List of entries to execute sequentially.
    /// </summary>
    public EnemySpawnEventListEntry[] entries;
}

/// <summary>
/// The EnemySpawnEventListEntry represents an entry of a EnemySpawnEventList.
/// It contains an array of events that should be executed.
/// An interval to the next event.
/// And the number of times it should be repeated before continuing to the next event
/// </summary>
[System.Serializable]
public class EnemySpawnEventListEntry
{
    /// <summary>
    /// The name of the entry. Useful to see it in the editor.
    /// </summary>
    public string entryName = "";

    /// <summary>
    /// The time needed to pass for the next event.
    /// </summary>
    public float timeTillNextEvent = 0.75f;

    /// <summary>
    /// The number of times this event should be repeated
    /// </summary>
    public int numberOfTimes = 0;

    /// <summary>
    /// The array of events to execute by a EnemySpawnManager.
    /// </summary>
    public EnemySpawnEvent[] spawnEvents;

}
