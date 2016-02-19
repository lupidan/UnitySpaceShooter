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

    public GameObject mainMenu = null;
    public GameObject gameMenu = null;
    public GameObject pauseMenu = null;
    public GameObject iconKeyboardImage = null;
    public GameObject iconMouseImage = null;

    // Use this for initialization
    void Start ()
    {
        this.visibleOption = Option.MainMenu;
	}
	
	// Update is called once per frame
	void Update () {
	
        switch (visibleOption)
        {
            case Option.GameMenu:
                //UPDATE UI HERE
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
