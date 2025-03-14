using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDisabler : MonoBehaviour
{
    public GameObject disabler;
    public GameObject enabler;

    public void CanvasDisabler()
    {
        StartCoroutine(DisableAfterSeconds(0.8f));
    }

    private IEnumerator DisableAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        disabler.SetActive(false);
        enabler.SetActive(true);
    }
}
