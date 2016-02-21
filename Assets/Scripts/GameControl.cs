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

public class GameControl : MonoBehaviour {

    /// <summary>
    /// The pool manager to use to Spawn GameObjects.
    /// </summary>
    public GameObjectPoolManager poolManager = null;

    /// <summary>
    /// The main game enemy spawn manager. Responsible of spawing all the enemies.
    /// </summary>
    public EnemySpawnManager enemySpawnManager = null;

    /// <summary>
    /// The Game Menu.
    /// </summary>
    public GameMenu gameMenu = null;

    /// <summary>
    /// The Highscores manager, responsible of keeping the highscores table.
    /// </summary>
    public HighscoresManager highscoresManager = null;

    /// <summary>
    /// The initial position for the player.
    /// </summary>
    public Transform playerStartPosition = null;

    /// <summary>
    /// The initial number of lives.
    /// </summary>
    public int initialLives = 0;

    /// <summary>
    /// The player score.
    /// </summary>
    [HideInInspector]
    public int score = 0;

    /// <summary>
    /// The number of lives available.
    /// </summary>
    [HideInInspector]
    public int lives = 0;

    /// <summary>
    /// The player ship for this game.
    /// </summary>
    public PlayerShip playerShip { get; private set; }

    /// <summary>
    /// Spawns a player on the screen.
    /// </summary>
    public void SpawnPlayer()
    {
        GameObject playerShipGameObject = poolManager.SpawnPrefabNamed("PlayerShip");
        playerShipGameObject.transform.position = playerStartPosition.position;
        PlayerShip playerShip = playerShipGameObject.GetComponent<PlayerShip>();
        if (playerShip != null)
        {
            playerShip.SetInvincibleForTime(1.0f);
            playerShip.poolManager = poolManager;
            playerShip.gameControl = this;

            this.playerShip = playerShip;
        }
    }

    /// <summary>
    /// Spawns a player after a number of seconds.
    /// </summary>
    /// <param name="numberOfSeconds">The number of seconds to wait to spawn a player.</param>
    public void SpawnPlayerInTime(float numberOfSeconds)
    {
        CancelInvoke("SpawnPlayer");
        Invoke("SpawnPlayer", numberOfSeconds);
    }

    /// <summary>
    /// Starts a game.
    /// </summary>
    public void StartGame()
    {
        lives = initialLives;
        score = 0;
        poolManager.RecycleAllSpawnedObjects();
        SpawnPlayer();
        enemySpawnManager.StartEvents();
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// Unpauses the game.
    /// </summary>
    public void UnpauseGame()
    {
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Ends the game.
    /// </summary>
    public void EndGame()
    {
        poolManager.RecycleAllSpawnedObjects();
        enemySpawnManager.CancelEvents();
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Executes Game Over.
    /// </summary>
    public void GameOver()
    {
        highscoresManager.AddHighscore("LUPI", score);
        gameMenu.VisibleOption = GameMenu.Option.GameOverMenu;
        enemySpawnManager.CancelEvents();
        Time.timeScale = 1.0f;
    }

}
