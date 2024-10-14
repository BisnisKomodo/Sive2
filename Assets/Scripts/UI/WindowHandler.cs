using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHandler : MonoBehaviour
{
    [HideInInspector]public InventoryManager inventory;
    [HideInInspector]public CraftingManager crafting;
    [HideInInspector]public StorageUI storage;
    public BuildingHandler building;
    public bool WindowOpened;
    private CameraLook cam;

    private void Start()
    {
        cam = GetComponentInChildren<CameraLook>();
        inventory = GetComponentInChildren<InventoryManager>();
        crafting = GetComponentInChildren<CraftingManager>();
        storage = GetComponentInChildren<StorageUI>();
        building = GetComponentInChildren<BuildingHandler>();
    }

    private void FixedUpdate()
    {
        if (WindowOpened)
        {
            cam.lockCursor = false;
            cam.canMove = false;
        }

        else
        {
            cam.lockCursor = true;
            cam.canMove = true;
        }

        if(inventory.opened)
        {
            WindowOpened = true;
        }
        else
        {
            WindowOpened = false;
        }
    }
}
