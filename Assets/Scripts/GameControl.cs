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
    /// Delegate for events related to the Game Control
    /// </summary>
    /// <param name="gameControl">The GameControl where the event happened</param>
    public delegate void GameControlEvent(GameControl gameControl);



    /// <summary>
    /// Event when the game starts.
    /// </summary>
    public event GameControlEvent OnGameStart;

    /// <summary>
    /// Event called when the control mode is changed.
    /// </summary>
    public event GameControlEvent OnControlModeChange;

    /// <summary>
    /// Event that happens when the player exits the whole game.
    /// </summary>
    public event GameControlEvent OnGameQuit;
    
    /// <summary>
    /// Event when the game is paused.
    /// </summary>
    public event GameControlEvent OnGamePause;
    
    /// <summary>
    /// Event when the game continues.
    /// </summary>
    public event GameControlEvent OnGameContinue;

    /// <summary>
    /// Event when the player ends the current game to the game menu.
    /// </summary>
    public event GameControlEvent OnGameEnd;

    /// <summary>
    /// Event when the game has finished.
    /// </summary>
    public event GameControlEvent OnGameFinished;

    /// <summary>
    /// Event called when the score is updated.
    /// </summary>
    public event GameControlEvent OnScoreUpdate;

    /// <summary>
    /// Event called when the number of lives is updated.
    /// </summary>
    public event GameControlEvent OnLivesUpdate;



    /// <summary>
    /// The player ship prefab to create the player.
    /// </summary>
    public GameObject playerShipPrefab = null;

    /// <summary>
    /// The initial position for the player.
    /// </summary>
    public Transform playerStartPosition = null;

    /// <summary>
    /// The initial number of lives.
    /// </summary>
    public int initialLives = 0;
    
    private int score = 0;

    /// <summary>
    /// The player score.
    /// </summary>
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            score = value;

            if (OnScoreUpdate != null)
            {
                OnScoreUpdate(this);
            }
        }
    }

    private int lives = 0;

    /// <summary>
    /// The number of lives available.
    /// </summary>
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            lives = value;

            if (OnLivesUpdate != null)
            {
                OnLivesUpdate(this);
            }
        }
    }

    /// <summary>
    /// The player ship for this game.
    /// </summary>
    public PlayerShip playerShip { get; private set; }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Toolbox.GameMenu.VisibleOption == GameMenu.Option.GameMenu)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }



    /// <summary>
    /// Spawns a player on the screen.
    /// </summary>
    public void SpawnPlayer()
    {
        GameObject playerShipGameObject = Toolbox.PoolManager.SpawnGameObject(playerShipPrefab);
        playerShipGameObject.transform.position = playerStartPosition.position;
        PlayerShip playerShip = playerShipGameObject.GetComponent<PlayerShip>();
        if (playerShip != null)
        {
            playerShip.SetInvincibleForTime(1.0f);

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
        Lives = initialLives;
        Score = 0;
        Toolbox.PoolManager.RecycleAllSpawnedObjects();
        SpawnPlayer();
        Toolbox.EnemySpawnManager.StartEvents();
        Time.timeScale = 1.0f;

        if (OnGameStart != null)
        {
            OnGameStart(this);
        }
    }

    /// <summary>
    /// Switches the control mode
    /// </summary>
    public void SwitchControlMode()
    {
        GamePlayerPrefs.IsMouseControlEnabled = !GamePlayerPrefs.IsMouseControlEnabled;

        if (OnControlModeChange != null)
        {
            OnControlModeChange(this);
        }
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        if (OnGameQuit != null)
        {
            OnGameQuit(this);
        }

        Application.Quit();
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0.0f;

        if (OnGamePause != null)
        {
            OnGamePause(this);
        }
    }

    /// <summary>
    /// Continues the game.
    /// </summary>
    public void ContinueGame()
    {
        Time.timeScale = 1.0f;

        if (OnGameContinue != null)
        {
            OnGameContinue(this);
        }
    }

    /// <summary>
    /// Ends the game.
    /// </summary>
    public void EndGame()
    {
        Toolbox.PoolManager.RecycleAllSpawnedObjects();
        Toolbox.EnemySpawnManager.CancelEvents();
        Time.timeScale = 1.0f;

        if (OnGameEnd != null)
        {
            OnGameEnd(this);
        }
    }

    /// <summary>
    /// Executes Game Over.
    /// </summary>
    public void FinishGame()
    {
        Toolbox.HighscoresManager.AddHighscore("LUPI", score);
        Toolbox.EnemySpawnManager.CancelEvents();
        Time.timeScale = 1.0f;

        if (OnGameFinished != null)
        {
            OnGameFinished(this);
        }
    }
    
}
