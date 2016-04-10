using UnityEngine;
using System.Collections;

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
