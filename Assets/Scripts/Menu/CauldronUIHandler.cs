using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronUIHandler : MonoBehaviour
{
    public Button closeButton;
    private WindowHandler windowHandler;

    private void Start()
    {
        windowHandler = GetComponentInParent<WindowHandler>();

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseCauldronUI);
        }
    }

    private void CloseCauldronUI()
    {
        UIManager.instance.SetCauldronCraftingUI(false);
        windowHandler.SetCauldronUIState(false);
    }
}
