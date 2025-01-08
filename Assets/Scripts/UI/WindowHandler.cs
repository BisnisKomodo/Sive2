using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class WindowHandler : MonoBehaviour
// {
//     [HideInInspector]public InventoryManager inventory;
//     [HideInInspector]public CraftingManager crafting;
//     [HideInInspector]public StorageUI storage;
//     [HideInInspector]public Gamemenu gameMenu;
//     public BuildingHandler building;
//     public bool WindowOpened;
//     private CameraLook cam;

//     private void Start()
//     {
//         cam = GetComponentInChildren<CameraLook>();
//         inventory = GetComponentInChildren<InventoryManager>();
//         crafting = GetComponentInChildren<CraftingManager>();
//         storage = GetComponentInChildren<StorageUI>();
//         building = GetComponentInChildren<BuildingHandler>();
//         gameMenu = FindObjectOfType<Gamemenu>();
//     }

//     private void FixedUpdate()
//     {
//         if (WindowOpened)
//         {
//             cam.lockCursor = false;
//             cam.canMove = false;
//         }

//         else
//         {
//             cam.lockCursor = true;
//             cam.canMove = true;
//         }

//         if (gameMenu != null)
//         {
//             if(inventory.opened || gameMenu.opened)
//             {
//                 WindowOpened = true;
//             }
//             else
//             {
//                 WindowOpened = false;
//             }
//         }
//         else
//         {
//             if(inventory.opened)
//             {
//                 WindowOpened = true;
//             }
//             else
//             {
//                 WindowOpened = false;
//             }
//         }
        
//     }
// }

public class WindowHandler : MonoBehaviour
{
    [HideInInspector] public InventoryManager inventory;
    [HideInInspector] public CraftingManager crafting;
    [HideInInspector] public StorageUI storage;
    [HideInInspector] public Gamemenu gameMenu;
    public BuildingHandler building;
    public bool WindowOpened;
    private CameraLook cam;

    
    [HideInInspector] public bool armoryUIOpened;
    [HideInInspector] public bool cauldronUIOpened;
    [HideInInspector] public bool benchUIOpened;

    private void Start()
    {
        cam = GetComponentInChildren<CameraLook>();
        inventory = GetComponentInChildren<InventoryManager>();
        crafting = GetComponentInChildren<CraftingManager>();
        storage = GetComponentInChildren<StorageUI>();
        building = GetComponentInChildren<BuildingHandler>();
        gameMenu = FindObjectOfType<Gamemenu>();
    }

    private void FixedUpdate()
    {
        
        if (inventory.opened || gameMenu.opened || armoryUIOpened || cauldronUIOpened || benchUIOpened)
        {
            WindowOpened = true;
        }
        else
        {
            WindowOpened = false;
        }

        
        cam.lockCursor = !WindowOpened;
        cam.canMove = !WindowOpened;
    }


    public void SetArmoryUIState(bool isOpen)
    {
        armoryUIOpened = isOpen;
        Cursor.visible = isOpen;
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void SetCauldronUIState(bool isOpen)
    {
        cauldronUIOpened = isOpen;
        Cursor.visible = isOpen;
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void SetBenchUIState(bool isOpen)
    {
        benchUIOpened = isOpen;
        Cursor.visible = isOpen;
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
