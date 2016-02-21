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
        PauseMenu
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
            gameMenu.SetActive(_visibleOption == Option.GameMenu);
            pauseMenu.SetActive(_visibleOption == Option.PauseMenu);
            UpdateControlIcon();
        }
    }

    public GameControl gameControl = null;
    public GameObject mainMenu = null;
    public GameObject gameMenu = null;
    public GameObject pauseMenu = null;
    public GameObject iconKeyboardImage = null;
    public GameObject iconMouseImage = null;
    public Text scoreText = null;
    public Text livesText = null;

    // Use this for initialization
    void Start ()
    {
        this.visibleOption = Option.GameMenu;
	}
	
	// Update is called once per frame
	void Update () {
	
        switch (visibleOption)
        {
            case Option.GameMenu:
                livesText.text = gameControl.lives.ToString("D2");
                scoreText.text = gameControl.score.ToString("D8");
                break;
        }

	}

    public void ButtonSelectedStartGame()
    {
        Debug.Log("START GAME");
    }

    public void ButtonSelectedChangeControlMode()
    {
        GamePlayerPrefs.IsMouseControlEnabled = !GamePlayerPrefs.IsMouseControlEnabled;
        UpdateControlIcon();
    }

    public void ButtonSelectedExitGame()
    {
        Debug.Log("EXIT GAME");
    }

    public void ButtonSelectedContinueGame()
    {
        Debug.Log("CONTINUE GAME");
    }

    public void ButtonSelectedEndGame()
    {
        Debug.Log("END GAME");
    }

    private void UpdateControlIcon()
    {
        bool isMouseControlEnabled = GamePlayerPrefs.IsMouseControlEnabled;
        iconKeyboardImage.SetActive(!isMouseControlEnabled);
        iconMouseImage.SetActive(isMouseControlEnabled);
    }
}
