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
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// The class Game Menu manages the Game UI
/// </summary>
public class GameMenu : MonoBehaviour {



    /// <summary>
    /// Enum type for the available menu options
    /// </summary>
    public enum Option
    {
        MainMenu,
        GameMenu,
        PauseMenu,
        GameOverMenu
    }



    /// <summary>
    /// Private visible option var
    /// </summary>
    private Option _visibleOption = Option.MainMenu;

    /// <summary>
    /// The menu Visible Option
    /// </summary>
    public Option VisibleOption
    {
        get
        {
            return _visibleOption;
        }
        set
        {
            _visibleOption = value;
            mainMenu.SetActive(_visibleOption == Option.MainMenu);
            gameMenu.SetActive(_visibleOption == Option.GameMenu || _visibleOption == Option.PauseMenu);
            pauseMenu.SetActive(_visibleOption == Option.PauseMenu);
            gameOverMenu.SetActive(_visibleOption == Option.GameOverMenu);

            UpdateHighscoresText();
        }
    }

    /// <summary>
    /// GameObject containing the main menu.
    /// </summary>
    public GameObject mainMenu = null;

    /// <summary>
    /// GameObject containing the game UI.
    /// </summary>
    public GameObject gameMenu = null;

    /// <summary>
    /// GameObject containing the pause menu.
    /// </summary>
    public GameObject pauseMenu = null;

    /// <summary>
    /// GameObject containing the game over menu.
    /// </summary>
    public GameObject gameOverMenu = null;

    /// <summary>
    /// GameObject containing the keyboard image icon.
    /// </summary>
    public GameObject iconKeyboardImage = null;

    /// <summary>
    /// GameObject containing the mouse image icon.
    /// </summary>
    public GameObject iconMouseImage = null;

    /// <summary>
    /// GameObject containing the button to change the control scheme.
    /// </summary>
    public GameObject controlSchemeButton = null;

    /// <summary>
    /// Text showing the score.
    /// </summary>
    public Text scoreText = null;

    /// <summary>
    /// Text showing the number of lives.
    /// </summary>
    public Text livesText = null;

    /// <summary>
    /// The highscores text.
    /// </summary>
    public Text highscoresText = null;

    /// <summary>
    /// Filled image showing the health of the ship.
    /// </summary>
    public Image playerHealthImage = null;




    void Start ()
    {
        this.VisibleOption = Option.MainMenu;

#if UNITY_ANDROID
        this.controlSchemeButton.SetActive(false);
#endif

        Toolbox.GameControl.OnGameStart += OnGameStart;
        Toolbox.GameControl.OnControlModeChange += OnControlModeChange;
        Toolbox.GameControl.OnGamePause += OnGamePause;
        Toolbox.GameControl.OnGameContinue += OnGameContinue;
        Toolbox.GameControl.OnGameEnd += OnGameEnd;
        Toolbox.GameControl.OnGameFinished += OnGameFinished;

        Toolbox.GameControl.OnScoreUpdate += OnScoreUpdate;
        Toolbox.GameControl.OnLivesUpdate += OnLivesUpdate;
        
        UpdateControlIcon();
        UpdateScoreText(0);
        UpdateLivesText(0);
        UpdateHealthImage(0.0f);
    }

    void OnDestroy()
    {
        Toolbox.GameControl.OnGameStart -= OnGameStart;
        Toolbox.GameControl.OnControlModeChange -= OnControlModeChange;
        Toolbox.GameControl.OnGamePause -= OnGamePause;
        Toolbox.GameControl.OnGameContinue -= OnGameContinue;
        Toolbox.GameControl.OnGameEnd -= OnGameEnd;
        Toolbox.GameControl.OnGameFinished -= OnGameFinished;

        Toolbox.GameControl.OnScoreUpdate -= OnScoreUpdate;
        Toolbox.GameControl.OnLivesUpdate -= OnLivesUpdate;
    }



    public void OnGameStart(GameControl gameControl)
    {
        VisibleOption = Option.GameMenu;

        gameControl.playerShip.OnHealthChange += OnHealthChange;
        UpdateHealthImage(1.0f);
    }

    public void OnControlModeChange(GameControl gameControl)
    {
        UpdateControlIcon();
    }

    public void OnGamePause(GameControl gameControl)
    {
        VisibleOption = Option.PauseMenu;
    }

    public void OnGameContinue(GameControl gameControl)
    {
        VisibleOption = Option.GameMenu;
    }

    public void OnGameEnd(GameControl gameControl)
    {
        VisibleOption = Option.MainMenu;

        gameControl.playerShip.OnHealthChange -= OnHealthChange;
    }

    public void OnGameFinished(GameControl gameControl)
    {
        VisibleOption = GameMenu.Option.GameOverMenu;

        gameControl.playerShip.OnHealthChange -= OnHealthChange;
    }

    public void OnScoreUpdate(GameControl gameControl)
    {
        UpdateScoreText(gameControl.Score);
    }

    public void OnLivesUpdate(GameControl gameControl)
    {
        UpdateLivesText(gameControl.Lives);
    }

    public void OnHealthChange(Ship ship)
    {
        UpdateHealthImage(ship.normalizedHealthPoints);
    }



    /// <summary>
    /// The start game was selected.
    /// </summary>
    public void ButtonStartGameSelected()
    {
        Toolbox.GameControl.StartGame();
    }

    /// <summary>
    /// The control mode switch button was selected.
    /// </summary>
    public void ButtonChangeControlModeSelected()
    {
        Toolbox.GameControl.SwitchControlMode();
    }

    /// <summary>
    /// The quit button was selected.
    /// </summary>
    public void ButtonQuitGameSelected()
    {
        Toolbox.GameControl.QuitGame();
    }

    /// <summary>
    /// The continue game button was selected.
    /// </summary>
    public void ButtonContinueGameSelected()
    {
        Toolbox.GameControl.ContinueGame();
    }

    /// <summary>
    /// The End Game button was selected.
    /// </summary>
    public void ButtonEndGameSelected()
    {
        Toolbox.GameControl.EndGame();
    }



    /// <summary>
    /// Updates the control icon to show the selected preference.
    /// </summary>
    private void UpdateControlIcon()
    {
        bool isMouseControlEnabled = GamePlayerPrefs.IsMouseControlEnabled;
        iconKeyboardImage.SetActive(!isMouseControlEnabled);
        iconMouseImage.SetActive(isMouseControlEnabled);
    }

    /// <summary>
    /// Updates the text shown for the highscores
    /// </summary>
    private void UpdateHighscoresText()
    {
        string text = "";
        List<HighscoresEntry> entries = Toolbox.HighscoresManager.table.entries;
        for (int i = 0; i < entries.Count; i++)
        {
            text += (i + 1) + ". " + entries[i].name + " - " + entries[i].score.ToString("D8") + "\n";
        }
        highscoresText.text = text;
    }

    /// <summary>
    /// Updates the lives text in the UI
    /// </summary>
    /// <param name="lives">The number of lives</param>
    private void UpdateLivesText(int lives)
    {
        livesText.text = lives.ToString("D2");
    }

    /// <summary>
    /// Updates the score in the UI
    /// </summary>
    /// <param name="score">The new score value</param>
    private void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString("D8");
    }

    /// <summary>
    /// Updates the health image percentage in the UI
    /// </summary>
    /// <param name="health">The amount of health, from 0 to 1</param>
    private void UpdateHealthImage(float health)
    {
        playerHealthImage.fillAmount = health;
    }

}
