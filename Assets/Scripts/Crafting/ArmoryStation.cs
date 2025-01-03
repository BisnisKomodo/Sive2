using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryStation : MonoBehaviour
{
    public GameObject craftingUI;
    public bool opened = false;
    public string armoryTag = "ArmoryCrafting";

    private void Start()
    {
        FindArmoryCraftingUI();
    }

    private void FindArmoryCraftingUI()
    {
        craftingUI = GameObject.FindWithTag(armoryTag); 
    }
    public void Open()
    {
        if (!opened)
        {
            if (craftingUI != null)
            {
                craftingUI.SetActive(true); 
            }

            opened = true; 
        }
    }
    public void Close()
    {
        if (opened)
        {
            if (craftingUI != null)
            {
                craftingUI.SetActive(false); 
            }

            opened = false; 
        }
    }
}
