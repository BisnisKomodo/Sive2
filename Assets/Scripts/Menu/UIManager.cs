using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject cauldronCraftingUI;
    public GameObject armoryCraftingUI;
    public GameObject benchCraftingUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCauldronCraftingUI(bool isActive)
    {
        if (cauldronCraftingUI != null)
        {
            cauldronCraftingUI.SetActive(isActive);
        }
    }

    public void SetArmoryCraftingUI(bool isActive)
    {
        if (armoryCraftingUI != null)
        {
            armoryCraftingUI.SetActive(isActive);
        }
    }

    public void SetBenchCraftingUI(bool isActive)
    {
        if (benchCraftingUI != null)
        {
            benchCraftingUI.SetActive(isActive);
        }
    }
}
