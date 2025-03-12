using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingReturn : MonoBehaviour
{
    public GameObject returning;
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
