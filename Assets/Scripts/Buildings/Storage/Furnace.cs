using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    public Storage storage;
    [Space]
    [Space]
    [HideInInspector] public StorageSlot fuelSlot;
    [HideInInspector] public StorageSlot smeltingSlot;
    [Space]
    public bool isOn;
    public GameObject VFX;
    [Space]

    private float currentFuelTimer;
    private float fuelTimer;

    private float currentSmeltTimer;
    private float smeltTimer;



    private void Start()
    {
        if (GetComponent<Storage>() != null)
        {
            storage = GetComponent<Storage>();
        }
        else
        {
            //Debug.Log("Furnace don't have access to storage scripts");
        }
    }

    // private void Update()
    // {
    //     #region Find slots

    //     if (isOn)
    //     {

    //         if (fuelSlot == null)
    //         {
    //             for (int i = 0; i < storage.slots.Length; i++)
    //             {
    //                 if (storage.slots[i].data != null)
    //                 {
    //                     if (storage.slots[i].data.isFuel)
    //                     {
    //                         fuelSlot = storage.slots[i];
    //                         currentFuelTimer = 0;
    //                         fuelTimer = fuelSlot.data.processTimer;
    //                         break;
    //                     }
    //                 }
    //             }
    //         }

    //         if (smeltingSlot == null)
    //         {
    //             for (int i = 0; i < storage.slots.Length; i++)
    //             {
    //                 if (storage.slots[i].data != null)
    //                 {
    //                     if (storage.slots[i].data.outcome != null)
    //                     {
    //                         smeltingSlot = storage.slots[i];
    //                         currentSmeltTimer = 0;
    //                         smeltTimer = smeltingSlot.data.processTimer;
    //                         break;
    //                     }
    //                 }
    //             }
    //         }

    //         if (fuelSlot == null)
    //         {
    //             TurnOff();
    //         }
    //         else
    //         {
    //             if (fuelSlot.data == null)
    //             {
    //                 TurnOff();
    //             }
    //         }
    //     }

    //     #endregion


    //     #region Smelting

    //     if (isOn)
    //     {
    //         //Fuel
    //         if (currentFuelTimer < fuelTimer)
    //         {
    //             currentFuelTimer += Time.deltaTime;
    //         }
    //         else
    //         {
    //             currentFuelTimer = 0;
    //             fuelSlot.stackSize--;
    //         }

    //         //Smelting
    //         if (currentSmeltTimer < smeltTimer)
    //         {
    //             currentSmeltTimer += Time.deltaTime;
    //         }
    //         else
    //         {
    //             currentSmeltTimer = 0;
    //             if (smeltingSlot != null)
    //             {
    //                 if (smeltingSlot.data != null)
    //                 {
    //                     storage.AddItem(smeltingSlot.data.outcome, smeltingSlot.data.outcomeAmount);
    //                 }
    //                 smeltingSlot.stackSize--;
    //             }
    //         }
    //     }
    //     #endregion
    // }

    // public void TurnOn()
    // {
    //     Debug.Log("Furnace Turned On!");
    //     isOn = true;
    //     if (VFX != null)
    //     {
    //         VFX.SetActive(true);
    //     }
    //     else
    //     {
    //         Debug.Log("VFX not assigned");
    //     }
    // }

    // public void TurnOff()
    // {
    //     isOn = false;
    //     VFX.SetActive(false);
    //     fuelSlot = null;
    //     smeltingSlot = null;
    // }

    private void Update()
    {
    #region Find slots

    if (isOn)
    {
        if (fuelSlot == null)
        {
            for (int i = 0; i < storage.slots.Length; i++)
            {
                if (storage.slots[i].data != null && storage.slots[i].data.isFuel)
                {
                    fuelSlot = storage.slots[i];
                    currentFuelTimer = 0;
                    fuelTimer = fuelSlot.data.processTimer;
                    break;
                }
            }
        }

        if (smeltingSlot == null)
        {
            for (int i = 0; i < storage.slots.Length; i++)
            {
                if (storage.slots[i].data != null && storage.slots[i].data.outcome != null)
                {
                    smeltingSlot = storage.slots[i];
                    currentSmeltTimer = 0;
                    smeltTimer = smeltingSlot.data.processTimer;
                    break;
                }
            }
        }

        // Turn off if no valid fuel slot
        if (fuelSlot == null || fuelSlot.data == null)
        {
            TurnOff();
        }
    }

    #endregion

    #region Smelting

    if (isOn)
    {
        // Ensure VFX is active while furnace is on
        if (VFX != null && !VFX.activeSelf)
        {
            VFX.SetActive(true);
        }

        // Fuel handling
        if (currentFuelTimer < fuelTimer)
        {
            currentFuelTimer += Time.deltaTime;
        }
        else
        {
            currentFuelTimer = 0;
            fuelSlot.stackSize--;

            // If fuel runs out, turn off the furnace
            if (fuelSlot.stackSize <= -1)
            {
                fuelSlot.data = null;
                TurnOff();
            }
        }

        // Smelting handling
        if (currentSmeltTimer < smeltTimer)
        {
            currentSmeltTimer += Time.deltaTime;
        }
        else
        {
            currentSmeltTimer = 0;
            if (smeltingSlot != null && smeltingSlot.data != null)
            {
                storage.AddItem(smeltingSlot.data.outcome, smeltingSlot.data.outcomeAmount);
                smeltingSlot.stackSize--;

                if (smeltingSlot.stackSize <= 0)
                {
                    smeltingSlot.data = null;
                    smeltingSlot = null;
                }
            }
        }
    }
    else
    {
        // Ensure VFX is off if the furnace is off
        if (VFX != null && VFX.activeSelf)
        {
            VFX.SetActive(false);
        }
    }

        #endregion
    }

    public void TurnOn()
    {
        //Debug.Log("Furnace Turned On!");
        isOn = true;

        if (VFX != null)
        {
            VFX.SetActive(true); // Turn on VFX when furnace starts
        }
        else
        {
            //Debug.LogWarning("VFX not assigned");
        }
    }

    public void TurnOff()
    {
        //Debug.Log("Furnace Turned Off!");
        isOn = false;

        if (VFX != null)
        {
            VFX.SetActive(false); // Turn off VFX when furnace stops
        }

        fuelSlot = null;
        smeltingSlot = null;
    }
}