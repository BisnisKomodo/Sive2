using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageUI : MonoBehaviour
{
    [HideInInspector] public Storage storageOpened;
    public Slot slotPrefab;
    public Transform content;
    [Space]
    public bool opened;
    public Vector3 openPosition;

    private void Update()
    {
        if (opened)
        {
            transform.localPosition = openPosition;
        }
        else
        {
            transform.position = new Vector3(-10000, 0, 0);
        }
    }


    public void Open(Storage storage)
    {
        storageOpened = storage;

        for (int i = 0; i < storage.slots.Length; i++)
        {
            Slot slot = Instantiate(slotPrefab, content).GetComponentInParent<Slot>();

            if (storage.slots[i].data == null)
            {
                slot.data = null;
            }
            else
            {
                slot.data = storage.slots[i].data;
            }

            slot.StackSize = storage.slots[i].stackSize;

            slot.UpdateSlot();
        }   

        opened = true;
    }

    public void Close()
    {
        if (storageOpened == null)
        {
            return;
        }


        storageOpened.Close(GetComponentsInChildren<Slot>());

        Slot[] slotsToDestroy = GetComponentsInChildren<Slot>();

        for (int i = 0; i < slotsToDestroy.Length; i++)
        {
            Destroy(slotsToDestroy[i].gameObject);
        }

        opened = false;
    }
}
