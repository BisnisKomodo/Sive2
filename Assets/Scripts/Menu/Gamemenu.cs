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
    public GameObject loadingScreenActivate;
    public Transform UI;
    public GameObject introButton;
    public GameObject playButton;
    [Space]
    [Space] 
    public GameObject backToMainMenuButton;
    public GameObject continueButton;
    public GameObject settingMenu;
    public GameObject exitButton;
    public GameObject settingButton;
    [Header("Main Menu")]
    public GameObject mainBackground;
    public GameObject mainBackground2;

    [Header("Pause Menu")]
    public GameObject pauseBackground;
    public Canvas pauseMenuCanvas;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // public void Update() 
    // {
    //     if (menuMode == MenuMode.Main)
    //     {
    //         UI.transform.localPosition = new Vector3(0, 0, 0);
    //     }
    //     else if (menuMode == MenuMode.Pause)
    //     {
    //         if (Input.GetKeyDown(KeyCode.Escape))
    //         {
    //             opened = !opened;
    //         }
    //         if (opened)
    //         {
    //             UI.transform.localPosition = new Vector3(0 ,0 ,0);
    //         }
    //         else
    //         {
    //             UI.transform.localPosition = new Vector3(-10000, 0, 0);
    //         }
    //     }
    // }

    public void TogglePauseMenu()
    {
        if (opened)
        {
            pauseMenuCanvas.sortingOrder = 10; 
            UI.transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            pauseMenuCanvas.sortingOrder = 0;
            UI.transform.localPosition = new Vector3(-10000, 0, 0);
        }
    }

    private void Update()
    {
        // For Pause Menu
        if (menuMode == MenuMode.Pause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                opened = !opened;
                UpdateMenuVisibility();
                TogglePauseMenu();
            }
        }
    }

    private void UpdateMenuVisibility()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // Main Menu Mode
        if (menuMode == MenuMode.Main && currentScene == "MainMenu")
        {
            UI.transform.localPosition = Vector3.zero; // Show UI
            mainBackground.SetActive(true);
            pauseBackground.SetActive(false);
            mainBackground2.SetActive(true);

            introButton.SetActive(true);
            playButton.SetActive(true);
            backToMainMenuButton.SetActive(false);
            continueButton.SetActive(false);
            settingMenu.SetActive(false);
        }
        // Pause Menu Mode
        else if (menuMode == MenuMode.Pause && currentScene != "MainMenu")
        {
            if (opened)
            {
                UI.transform.localPosition = Vector3.zero; // Show UI
                mainBackground.SetActive(false);
                pauseBackground.SetActive(true);
                mainBackground2.SetActive(false);

                introButton.SetActive(false);
                playButton.SetActive(false);
                backToMainMenuButton.SetActive(true);
                continueButton.SetActive(true);
                settingMenu.SetActive(false);
                exitButton.SetActive(false);
            }
            else
            {
                UI.transform.localPosition = new Vector3(-10000, 0, 0); // Hide UI
            }
        }
    }

    public void Intro()
    {
        SceneManager.LoadScene(4);
        DestroyAllDontDestroyOnLoadObjects();
    }

    public void StartGame()
    {
        loadingScreenActivate.gameObject.SetActive(true);
        menuMode = MenuMode.Pause;
        UI.transform.localPosition = new Vector3(-10000, 0, 0);
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

        continueButton.SetActive(false);
        backToMainMenuButton.SetActive(false);
        settingButton.SetActive(false);
    }
    
    public void Exit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void CloseEscape()
    {
        UI.transform.localPosition = new Vector3(-10000, 0, 0);
    }

    private void DestroyAllDontDestroyOnLoadObjects()
    {
    GameObject temp = new GameObject();
    Scene tempScene = temp.scene;

    foreach (GameObject obj in FindObjectsOfType<GameObject>())
    {
        if (obj.scene != tempScene) // Objects in DontDestroyOnLoad exist in a different scene
        {
            Destroy(obj);
        }
    }

    Destroy(temp);
    }
}
