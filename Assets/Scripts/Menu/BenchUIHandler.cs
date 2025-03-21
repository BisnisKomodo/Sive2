using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BenchUIHandler : MonoBehaviour
{
    public Button closeButton;
    private WindowHandler windowHandler;

    private void Start()
    {
        windowHandler = GetComponentInParent<WindowHandler>();

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseBenchUI);
        }
    }

    private void CloseBenchUI()
    {
        UIManager.instance.SetBenchCraftingUI(false);
        windowHandler.SetBenchUIState(false);
    }
}
