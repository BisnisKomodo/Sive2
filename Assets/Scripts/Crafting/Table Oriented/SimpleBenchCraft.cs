using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBenchCraft : MonoBehaviour
{
    public bool opened = false;
    public void Open()
    {
        if (!opened)
        {
            UIManager.instance.SetBenchCraftingUI(true);
            FindObjectOfType<WindowHandler>().SetBenchUIState(true);
            opened = true;
        }
    }

    public void Close()
    {
        if (opened)
        {
            UIManager.instance.SetBenchCraftingUI(false);
            FindObjectOfType<WindowHandler>().SetBenchUIState(false);
            opened = false;
        }
    }
}
