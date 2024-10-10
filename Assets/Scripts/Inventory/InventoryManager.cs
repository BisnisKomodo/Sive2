using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Weapon[] weapons;
    public bool opened;
    public KeyCode InventoryKey = KeyCode.Tab;

    [Header("Settings")]
    public int inventorySize = 24;
    public int hotbarSize = 6;


    [Header("Refs")]
    public GameObject dropModel;
    public Transform dropPos;
    public GameObject slotTemplate;
    public Transform contentHolder;
    public Transform hotbarContentHolder;




    [Space]
    [HideInInspector] public Slot[] inventorySlots;
    [SerializeField] private Slot[] allSlots;
    private Slot[] hotbarSlots;

    private void Start()
    {
        GenerateHotBarSlots();
        GenerateSlots();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hotbarSlots[0].Try_Use();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hotbarSlots[1].Try_Use();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hotbarSlots[2].Try_Use();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            hotbarSlots[3].Try_Use();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            hotbarSlots[4].Try_Use();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            hotbarSlots[5].Try_Use();
        }













        if (Input.GetKeyDown(InventoryKey))
        {
            opened = !opened;
        }
        
        if(opened)
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }

        else
        {
            transform.localPosition = new Vector3(-10000, 0, 0);
        }
    }

    private void GenerateSlots()
    {
        List<Slot> inventorySlots_ = new List<Slot>();
        List<Slot> allSlots_ = new List<Slot>();

        //Semua slot array masuk ke list
        for(int i = 0; i < allSlots.Length; i++)
        {
            allSlots_.Add(allSlots[i]);
        }

        //Slot Generator
        for(int i = 0; i < inventorySize; i++)
        {
            Slot slot = Instantiate(slotTemplate.gameObject, contentHolder).GetComponent<Slot>();

            inventorySlots_.Add(slot);
            allSlots_.Add(slot);
        }

        inventorySlots = inventorySlots_.ToArray();
        allSlots = allSlots_.ToArray();
    }

    private void GenerateHotBarSlots()
    {
        List<Slot> inventorySlots_ = new List<Slot>();
        List<Slot> hotbarList = new List<Slot>();

        //Slot Generator
        for(int i = 0; i < hotbarSize; i++)
        {
            Slot slot = Instantiate(slotTemplate.gameObject, hotbarContentHolder).GetComponent<Slot>();

            inventorySlots_.Add(slot);
            hotbarList.Add(slot);
        }

        inventorySlots = inventorySlots_.ToArray();
        hotbarSlots = hotbarList.ToArray();
    }

    public void DragDrop(Slot from, Slot to)
    {
        //Ngeswap barang
        if(from.data != to.data)
        {
            ItemScriptableObject data = to.data;
            int StackSize = to.StackSize;

            to.data = from.data;
            to.StackSize = from.StackSize;

            from.data = data;
            from.StackSize = StackSize;
        }
        //Stacking
        else
        {
            if (from.data.IsStackable)
            {
                if(from.StackSize + to.StackSize > from.data.maxstack)
                {
                    int amountLeft = from.StackSize + to.StackSize - from.data.maxstack;

                    from.StackSize = amountLeft;
                    to.StackSize = to.data.maxstack;
                }
            }
            else
            {
                ItemScriptableObject data = to.data;
                int StackSize = to.StackSize;

                to.data = from.data;
                to.StackSize = from.StackSize;

                from.data = data;
                from.StackSize = StackSize;
            }
        }

        from.UpdateSlot();
        to.UpdateSlot();
    }

    public void AddItem(Pickup pickUp)
    {
        if (pickUp.data.IsStackable)
        {
            Slot stackableSlot = null;

            // TRY FINDING STACKABLE SLOT
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (!inventorySlots[i].IsEmpty)
                {
                    if (inventorySlots[i].data == pickUp.data && inventorySlots[i].StackSize < pickUp.data.maxstack)
                    {
                        stackableSlot = inventorySlots[i];
                        break;
                    }

                }
            }

            if (stackableSlot != null)
            {

                // IF IT CANNOT FIT THE PICKED UP AMOUNT
                if (stackableSlot.StackSize + pickUp.StackSize > pickUp.data.maxstack)
                {
                    int amountLeft = (stackableSlot.StackSize + pickUp.StackSize) - pickUp.data.maxstack;



                    // ADD IT TO THE STACKABLE SLOT
                    stackableSlot.AddItemToSlot(pickUp.data, pickUp.data.maxstack);

                    // TRY FIND A NEW EMPTY STACK
                    for (int i = 0; i < inventorySlots.Length; i++)
                    {
                        if (inventorySlots[i].IsEmpty)
                        {
                            inventorySlots[i].AddItemToSlot(pickUp.data, amountLeft);
                            inventorySlots[i].UpdateSlot();

                            break;
                        }
                    }



                    Destroy(pickUp.gameObject);
                }
                // IF IT CAN FIT THE PICKED UP AMOUNT
                else
                {
                    stackableSlot.AddStackAmount(pickUp.StackSize);

                    Destroy(pickUp.gameObject);
                }

                stackableSlot.UpdateSlot();
            }
            else
            {
                Slot emptySlot = null;


                // FIND EMPTY SLOT
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (inventorySlots[i].IsEmpty)
                    {
                        emptySlot = inventorySlots[i];
                        break;
                    }
                }

                // IF WE HAVE AN EMPTY SLOT THAN ADD THE ITEM
                if (emptySlot != null)
                {
                    emptySlot.AddItemToSlot(pickUp.data, pickUp.StackSize);
                    emptySlot.UpdateSlot();

                    Destroy(pickUp.gameObject);
                }
                else
                {
                    pickUp.transform.position = dropPos.position;
                }
            }

        }
        else
        {
            Slot emptySlot = null;


            // FIND EMPTY SLOT
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].IsEmpty)
                {
                    emptySlot = inventorySlots[i];
                    break;
                }
            }

            // IF WE HAVE AN EMPTY SLOT THAN ADD THE ITEM
            if (emptySlot != null)
            {
                emptySlot.AddItemToSlot(pickUp.data, pickUp.StackSize);
                emptySlot.UpdateSlot();

                Destroy(pickUp.gameObject);
            }
            else
            {
                pickUp.transform.position = dropPos.position;
            }

        }
    }


    public void AddItem(ItemScriptableObject data, int StackSize)
    {
        if (data.IsStackable)
        {
            Slot stackableSlot = null;

            // TRY FINDING STACKABLE SLOT
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (!inventorySlots[i].IsEmpty)
                {
                    if (inventorySlots[i].data == data && inventorySlots[i].StackSize < data.maxstack)
                    {
                        stackableSlot = inventorySlots[i];
                        break;
                    }

                }
            }

            if (stackableSlot != null)
            {

                // IF IT CANNOT FIT THE PICKED UP AMOUNT
                if (stackableSlot.StackSize + StackSize > data.maxstack)
                {
                    int amountLeft = (stackableSlot.StackSize + StackSize) - data.maxstack;



                    // ADD IT TO THE STACKABLE SLOT
                    stackableSlot.AddItemToSlot(data, data.maxstack);

                    // TRY FIND A NEW EMPTY STACK
                    for (int i = 0; i < inventorySlots.Length; i++)
                    {
                        if (inventorySlots[i].IsEmpty)
                        {
                            inventorySlots[i].AddItemToSlot(data, amountLeft);
                            inventorySlots[i].UpdateSlot();

                            break;
                        }
                    }



                    
                }
                // IF IT CAN FIT THE PICKED UP AMOUNT
                else
                {
                    stackableSlot.AddStackAmount(StackSize);

                }

                stackableSlot.UpdateSlot();
            }
            else
            {
                Slot emptySlot = null;


                // FIND EMPTY SLOT
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (inventorySlots[i].IsEmpty)
                    {
                        emptySlot = inventorySlots[i];
                        break;
                    }
                }

                // IF WE HAVE AN EMPTY SLOT THAN ADD THE ITEM
                if (emptySlot != null)
                {
                    emptySlot.AddItemToSlot(data, StackSize);
                    emptySlot.UpdateSlot();

                }
                else
                {
                    DropItem(data, StackSize);
                }
            }

        }
        else
        {
                Slot emptySlot = null;


                // FIND EMPTY SLOT
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (inventorySlots[i].IsEmpty)
                    {
                        emptySlot = inventorySlots[i];
                        break;
                    }
                }

                // IF WE HAVE AN EMPTY SLOT THAN ADD THE ITEM
                if (emptySlot != null)
                {
                    emptySlot.AddItemToSlot(data, StackSize);
                    emptySlot.UpdateSlot();

                }
                else
                {
                    DropItem(data, StackSize);
                }
        }

    }
    

    public void DropItem(Slot slot)
    {
        Pickup pickup = Instantiate(dropModel, dropPos).AddComponent<Pickup>();
        pickup.transform.position = dropPos.position;
        pickup.transform.SetParent(null);

        pickup.data = slot.data;
        pickup.StackSize = slot.StackSize;

        slot.Clean();
    }


    public void DropItem(ItemScriptableObject data, int StackSize)
    {
        Pickup pickup = Instantiate(dropModel, dropPos).AddComponent<Pickup>();
        pickup.transform.position = dropPos.position;
        pickup.transform.SetParent(null);

        pickup.data = data;
        pickup.StackSize = StackSize;
    }
}



//----------------------------------------------SPLITTING STACK TEST------------------------------------------------//
