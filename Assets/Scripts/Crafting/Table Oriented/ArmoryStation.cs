using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryStation : MonoBehaviour
{
    public bool opened = false;
    public void Open()
    {
        if (!opened)
        {
            UIManager.instance.SetArmoryCraftingUI(true);
            FindObjectOfType<WindowHandler>().SetArmoryUIState(true);
            opened = true;
        }
    }
    public void Close()
    {
        if (opened)
        {
            UIManager.instance.SetArmoryCraftingUI(false);
            FindObjectOfType<WindowHandler>().SetArmoryUIState(false);
            opened = false; 
        }
    }
}
