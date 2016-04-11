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

public class Toolbox : MonoBehaviour {

    private static Toolbox instance;
    public static Toolbox Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Toolbox>();
            }
            return instance;
        }
    }

    /// <summary>
    /// Game Control instance of the main Toolbox instance
    /// </summary>
    public static GameControl GameControl { get { return Instance.gameControl; } }

    /// <summary>
    /// The Pool Manager for game objects instance of the main Toolbox instance
    /// </summary>
    public static GameObjectPoolManager PoolManager { get { return Instance.poolManager; } }

    /// <summary>
    /// The Game's menu instance of the main Toolbox instance
    /// </summary>
    public static GameMenu GameMenu { get { return Instance.gameMenu; } }

    /// <summary>
    /// The Enemy Spawn manager instance of the main Toolbox instance
    /// </summary>
    public static EnemySpawnManager EnemySpawnManager { get { return Instance.enemySpawnManager; } }

    /// <summary>
    /// The highscores manager instance of the main Toolbox instance
    /// </summary>
    public static HighscoresManager HighscoresManager { get { return Instance.highscoresManager; } }

    /// <summary>
    /// The Game Control instance for the game.
    /// </summary>
    public GameControl gameControl;

    /// <summary>
    /// The Game Menu instance of the game.
    /// </summary>
    public GameMenu gameMenu;

    /// <summary>
    /// The Game Object Pool Manager of the game.
    /// </summary>
    public GameObjectPoolManager poolManager;

    /// <summary>
    /// The highscores manager of the game.
    /// </summary>
    public HighscoresManager highscoresManager;

    /// <summary>
    /// The enemy spawner for the game.
    /// </summary>
    public EnemySpawnManager enemySpawnManager;

}
