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

    [Header("Drop Bag Configuration")]
    public Transform dropPos;
    public Pickup dropBag;


    [Header("Furnace Configuration")]
    [HideInInspector] public bool isFurnace; 

    public void OnDestroy()
    {
        Slot[] slots = GetComponentsInChildren<Slot>();

        for (int i = 0; i < slots.Length; i++)
        {
            DropItem(slots[i].data, slots[i].StackSize);
        }
    }

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

    public void AddItem(ItemScriptableObject data_, int stack_)
    {
        StorageSlot[] slots = GetComponentsInChildren<StorageSlot>();

        if (data_.IsStackable)
        {
            StorageSlot stackableSlot = null;

            // TRY FINDING STACKABLE SLOT
            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i].IsEmpty)
                {
                    if (slots[i].data == data_ && slots[i].stackSize < data_.maxstack)
                    {
                        stackableSlot = slots[i];
                        break;
                    }

                }
            }

            if (stackableSlot != null)
            {

                // IF IT CANNOT FIT THE PICKED UP AMOUNT
                if (stackableSlot.stackSize + stack_ > data_.maxstack)
                {
                    int amountLeft = (stackableSlot.stackSize + stack_) - data_.maxstack;



                    // ADD IT TO THE STACKABLE SLOT
                    stackableSlot.AddItemToSlot(data_, data_.maxstack);

                    // TRY FIND A NEW EMPTY STACK
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i].IsEmpty)
                        {
                            slots[i].AddItemToSlot(data_, amountLeft);
                            break;
                        }
                    }



                    
                }
                // IF IT CAN FIT THE PICKED UP AMOUNT
                else
                {
                    stackableSlot.AddStackAmount(stack_);

                }
            }
            else
            {
                StorageSlot emptySlot = null;


                // FIND EMPTY SLOT
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].IsEmpty)
                    {
                        emptySlot = slots[i];
                        break;
                    }
                }

                // IF WE HAVE AN EMPTY SLOT THAN ADD THE ITEM
                if (emptySlot != null)
                {
                    emptySlot.AddItemToSlot(data_, stack_);

                }
                else
                {
                    DropItem(data_, stack_);
                }
            }

        }
        else
        {
                StorageSlot emptySlot = null;


                // FIND EMPTY SLOT
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].IsEmpty)
                    {
                        emptySlot = slots[i];
                        break;
                    }
                }

                // IF WE HAVE AN EMPTY SLOT THAN ADD THE ITEM
                if (emptySlot != null)
                {
                    emptySlot.AddItemToSlot(data_, stack_);

                }
                else
                {
                    DropItem(data_, stack_);
                }
        }
    }

    public void DropItem(ItemScriptableObject data_, int stack_)
    {
        Pickup drop = Instantiate(dropBag.gameObject, dropPos).GetComponent<Pickup>();

        drop.transform.SetParent(null);

        drop.data = data_;
        drop.StackSize = stack_;
    }
}
