using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class Storage : MonoBehaviour
{
    public StorageSlot[] slots;
    [Space]
    bool opened;

    public void Open(StorageUI UI)
    {
        
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

}
