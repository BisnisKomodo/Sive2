using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    public GameObject craftingUI;
    public bool opened = false;
    //public string cauldronTag = "CauldronCrafting";

    private void Start()
    {
        FindCauldronCraftingUI();
    }

    private void FindCauldronCraftingUI()
    {
        //craftingUI = GameObject.FindWithTag(cauldronTag);
        craftingUI = GameObject.Find("CauldronCrafting");
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
