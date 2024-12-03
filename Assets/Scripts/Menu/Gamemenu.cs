using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemenu : MonoBehaviour
{
    public bool opened;
    public enum MenuMode{Main, Pause}
    public MenuMode menuMode;

    [Header("General")]
    public Transform UI;
    public GameObject introButton;
    public GameObject playButton;
    [Space]
    [Space] 
    public GameObject backToMainMenuButton;
    public GameObject settingMenu;
    [Header("Main Menu")]
    public GameObject mainBackground;

    [Header("Pause Menu")]
    public GameObject pauseBackground;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Update() 
    {
        if (menuMode == MenuMode.Main)
        {
            UI.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (menuMode == MenuMode.Pause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                opened = !opened;
            }
            if (opened)
            {
                UI.transform.localPosition = new Vector3(0 ,0 ,0);
            }
            else
            {
                UI.transform.localPosition = new Vector3(-10000, 0, 0);
            }
        }
    }

    public void Intro()
    {

    }

    public void StartGame()
    {
        menuMode = MenuMode.Pause;

        SceneManager.LoadScene(1);
    }

    public void BackToMain()
    {
        menuMode = MenuMode.Main;

        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void Settings()
    {
        settingMenu.SetActive(true);
    }
    
    public void Exit()
    {
        Application.Quit();
    }

}
