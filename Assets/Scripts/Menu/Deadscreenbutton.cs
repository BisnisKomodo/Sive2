using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deadscreenbutton : MonoBehaviour
{
    public GameObject retryButton;
    public GameObject exitButton;
    public GameObject canvasDisabler;
    public GameObject loadingEnabler;

    public void retryGame()
    {
        SceneManager.LoadScene(1);
        canvasDisabler.gameObject.SetActive(false);
        loadingEnabler.gameObject.SetActive(true);
    }

    public void exitGame()
    {
        SceneManager.LoadScene(0);
        canvasDisabler.gameObject.SetActive(false);
        loadingEnabler.gameObject.SetActive(true);
    }
}
