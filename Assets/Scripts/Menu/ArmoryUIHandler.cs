using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmoryUIHandler : MonoBehaviour
{
    public Button closeButton;
    private WindowHandler windowHandler;

    private void Start()
    {
        windowHandler = GetComponentInParent<WindowHandler>();

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ArmoryUI);
        }
    }

    private void ArmoryUI()
    {
        UIManager.instance.SetArmoryCraftingUI(false);
        windowHandler.SetArmoryUIState(false);
    }
}
