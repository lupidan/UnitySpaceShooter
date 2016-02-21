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
using System.Collections;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    public enum Option
    {
        MainMenu,
        GameMenu,
        PauseMenu,
        GameOverMenu
    }

    private Option _visibleOption = Option.MainMenu;
    public Option visibleOption
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

    public GameControl gameControl = null;
    public GameObject mainMenu = null;
    public GameObject gameMenu = null;
    public GameObject pauseMenu = null;
    public GameObject gameOverMenu = null;
    public GameObject iconKeyboardImage = null;
    public GameObject iconMouseImage = null;
    public Text scoreText = null;
    public Text livesText = null;
    public Image playerHealthImage = null;

    // Use this for initialization
    void Start ()
    {
        this.visibleOption = Option.MainMenu;
    }
	
	// Update is called once per frame
	void Update () {

        if (gameMenu.activeSelf)
        {
            UpdateGameMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (visibleOption == Option.GameMenu)
            {
                gameControl.PauseGame();
                visibleOption = Option.PauseMenu;
            }
            else
            {
                gameControl.UnpauseGame();
                visibleOption = Option.GameMenu;
            }
        }

	}

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

    public void ButtonSelectedStartGame()
    {
        gameControl.StartGame();
        visibleOption = Option.GameMenu;
    }

    public void ButtonSelectedChangeControlMode()
    {
        GamePlayerPrefs.IsMouseControlEnabled = !GamePlayerPrefs.IsMouseControlEnabled;
        UpdateControlIcon();
    }

    public void ButtonSelectedExitGame()
    {
        Application.Quit();
    }

    public void ButtonSelectedContinueGame()
    {
        gameControl.UnpauseGame();
        visibleOption = Option.GameMenu;
    }

    public void ButtonSelectedEndGame()
    {
        gameControl.EndGame();
        visibleOption = Option.MainMenu;
    }

    private void UpdateControlIcon()
    {
        bool isMouseControlEnabled = GamePlayerPrefs.IsMouseControlEnabled;
        iconKeyboardImage.SetActive(!isMouseControlEnabled);
        iconMouseImage.SetActive(isMouseControlEnabled);
    }
}
