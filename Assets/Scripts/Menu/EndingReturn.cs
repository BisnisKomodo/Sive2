using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingReturn : MonoBehaviour
{
    public GameObject returning;
    public GameObject disablerReturn;
    public GameObject enablerReturn;
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CanvasDisabler()
    {
        StartCoroutine(DisableAfterSeconds(0.8f));
    }

    private IEnumerator DisableAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        disablerReturn.SetActive(false);
        enablerReturn.SetActive(true);
    }
}
