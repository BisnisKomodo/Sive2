using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class Storage : MonoBehaviour
{
    private bool hasGenerateSlots;
    [HideInInspector] public StorageSlot[] slots;
    public StorageSlot slotPrefab;
    public int storageSize = 12;


    [Space]
    public bool opened;


    [Header("Furnace Configuration")]
    [HideInInspector] public bool isFurnace; 

    public void Start()
    {
        isFurnace = GetComponent<Furnace>() != null;

        if (!hasGenerateSlots)
        {
            GenerateSlots();
        }
        
        
        // List<StorageSlot> slotList = new List<StorageSlot>();
        // for (int i = 0; i < storageSize; i++)
        // {
        //     StorageSlot slot = Instantiate(slotPrefab, transform).GetComponent<StorageSlot>();

        //     slotList.Add(slot);
        // }

        // slots = slotList.ToArray();
    }

    private void Update() 
    {
        
    }

    public void GenerateSlots()
    {
        List<StorageSlot> slotList = new List<StorageSlot>();
        for (int i = 0; i < storageSize; i++)
        {
            StorageSlot slot = Instantiate(slotPrefab, transform).GetComponent<StorageSlot>();

            slotList.Add(slot);
        }

        slots = slotList.ToArray();

        hasGenerateSlots = true;
    }

    public void Open(StorageUI UI)
    {
        UI.Open(this);

        opened = true;
    }

    public void Close(Slot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            if (uiSlots[i] == null)
            {
                slots[i].data = null;
            }
            else
            {
                slots[i].data = uiSlots[i].data;
            }

            slots[i].stackSize = uiSlots[i].StackSize;
        }
        opened = false;
    }

    public void AddItem(ItemScriptableObject data, float stack_)
    {
        
    }

}
