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

            UpdateControlIcon();
        }
    }

    /// <summary>
    /// The Game Control.
    /// </summary>
    public GameControl gameControl = null;

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
    /// Text showing the score.
    /// </summary>
    public Text scoreText = null;

    /// <summary>
    /// Text showing the number of lives.
    /// </summary>
    public Text livesText = null;

    /// <summary>
    /// Filled image showing the health of the ship.
    /// </summary>
    public Image playerHealthImage = null;

    void Start ()
    {
        this.VisibleOption = Option.MainMenu;
    }
	
	void Update () {

        if (gameMenu.activeSelf)
        {
            UpdateGameMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (VisibleOption == Option.GameMenu)
            {
                gameControl.PauseGame();
                VisibleOption = Option.PauseMenu;
            }
            else
            {
                gameControl.UnpauseGame();
                VisibleOption = Option.GameMenu;
            }
        }

	}

    /// <summary>
    /// Updates the game menu UI.
    /// </summary>
    private void UpdateGameMenu()
    {
        livesText.text = gameControl.lives.ToString("D2");
        scoreText.text = gameControl.score.ToString("D8");
        if (gameControl.playerShip != null)
        {
            playerHealthImage.fillAmount = gameControl.playerShip.healthPoints / gameControl.playerShip.initialHealthPoints;
        }
        else
        {
            playerHealthImage.fillAmount = 0.0f;
        }
    }

    /// <summary>
    /// The start game was selected.
    /// </summary>
    public void ButtonSelectedStartGame()
    {
        gameControl.StartGame();
        VisibleOption = Option.GameMenu;
    }

    /// <summary>
    /// The control mode switch button was selected.
    /// </summary>
    public void ButtonSelectedChangeControlMode()
    {
        GamePlayerPrefs.IsMouseControlEnabled = !GamePlayerPrefs.IsMouseControlEnabled;
        UpdateControlIcon();
    }

    /// <summary>
    /// The exit button was selected.
    /// </summary>
    public void ButtonSelectedExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// The continue game button was selected.
    /// </summary>
    public void ButtonSelectedContinueGame()
    {
        gameControl.UnpauseGame();
        VisibleOption = Option.GameMenu;
    }

    /// <summary>
    /// The End Game button was selected.
    /// </summary>
    public void ButtonSelectedEndGame()
    {
        gameControl.EndGame();
        VisibleOption = Option.MainMenu;
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
}
