using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSlot : MonoBehaviour
{
    [HideInInspector] public bool IsEmpty;
    public ItemScriptableObject data;
    public int stackSize;
    
    private void Update()
    {
        IsEmpty = data == null;
    }

    public void AddItemToSlot(ItemScriptableObject data_, int StackSize_)
    {
        data = data_;
        stackSize = StackSize_;
    }

    public void AddStackAmount(int StackSize_)
    {
        stackSize += StackSize_;
    }
}
