// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;

// public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
// {
//     private DragDropHandler dragDropHandler;
//     private InventoryManager inventory;
//     public ItemScriptableObject data;
//     public int StackSize;
//     public Image icon;
//     public Text StackText;
//     public bool IsEmpty;
//     public bool isempty => IsEmpty;

//     private void Start()
//     {
//         dragDropHandler = GetComponentInParent<DragDropHandler>();
//         inventory = GetComponentInParent<InventoryManager>();
//         UpdateSlot();
//     }

//     public void UpdateSlot()
//     {
//         if (data == null)
//         {
//             IsEmpty = true;

//             icon.gameObject.SetActive(false);  
//             StackText.gameObject.SetActive(false);
//         }
//         else
//         {
//             IsEmpty = false;

//             icon.sprite = data.icon;
//             StackText.text = $"x{StackSize}";

//             icon.gameObject.SetActive(true);
//             StackText.gameObject.SetActive(true);
//         }
//     }

//     public void AddItemToSlot(ItemScriptableObject data_, int StackSize_)
//     {
//         data = data_;
//         StackSize = StackSize_;
//     }

//     public void AddStackAmount(int StackSize_)
//     {
//         StackSize += StackSize_;
//     }

//     public void Drop()
//     {
//         GetComponentInParent<InventoryManager>().DropItem(this);
//     }

//     public void Clean()
//     {
//         data = null;
//         StackSize = 0;

//         UpdateSlot();
//     }

//     public void OnPointerDown(PointerEventData eventData)
//     {
//         if (dragDropHandler.isDragging)
//         {
//             if (eventData.button == PointerEventData.InputButton.Left)
//             {
//                 dragDropHandler.slotDraggedFrom = this;
//                 dragDropHandler.isDragging = true;
//             }
//         }
//     }

//     public void OnPointerUp(PointerEventData eventData)
//     {
//         if (dragDropHandler.isDragging)
//         {
//             //DROP
//             if (dragDropHandler.slotDraggedTo == null)
//             {
//                 dragDropHandler.slotDraggedFrom.Drop();
//                 dragDropHandler.isDragging = false;
//             }

//             //DRAG AND DROP
//             else if (dragDropHandler.slotDraggedTo != null)
//             {
//                 inventory.DragDrop(dragDropHandler.slotDraggedFrom, dragDropHandler.slotDraggedTo);
//                 dragDropHandler.isDragging = false;
//             }
//         }
//     }

//     public void OnPointerEnter(PointerEventData eventData)
//     {
//         if (dragDropHandler.isDragging)
//         {
//             dragDropHandler.slotDraggedTo = this;
//         }
//     }

//     public void OnPointerExit(PointerEventData eventData)
//     {
//         if (dragDropHandler.isDragging)
//         {
//             dragDropHandler.slotDraggedTo = null;
//         }
//     }
// }



//------------------------------------------------------------------------------------------------------------------//



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private DragDropHandler dragDropHandler;
    public InventoryManager inventory;
    public Weapon weaponEquipped;
    public ItemScriptableObject data;
    public int StackSize;
    public Image icon;
    public Text StackText;
    public bool IsEmpty;
    public bool isempty => IsEmpty;

    private void Start()
    {
        dragDropHandler = GetComponentInParent<PlayerMovement>().GetComponentInChildren<DragDropHandler>();
        inventory = GetComponentInParent<PlayerMovement>().GetComponentInChildren<InventoryManager>();
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (data != null)
        {
            if (data.itemtype != ItemScriptableObject.ItemType.Weapon)
            {
                if (StackSize <= 0)
                {
                    data = null;
                }
            }
        }


        if (data == null)
        {
            IsEmpty = true;
            icon.gameObject.SetActive(false);
            StackText.gameObject.SetActive(false);
        }
        else
        {
            IsEmpty = false;
            icon.sprite = data.icon;
            StackText.text = $"{StackSize}";
            icon.gameObject.SetActive(true);
            StackText.gameObject.SetActive(true);
        }
    }

    public void AddItemToSlot(ItemScriptableObject data_, int StackSize_)
    {
        data = data_;
        StackSize = StackSize_;
        UpdateSlot(); 
    }

    public void AddStackAmount(int StackSize_)
    {
        StackSize += StackSize_;
        UpdateSlot(); 
    }

    public void Drop()
    {
        inventory.DropItem(this); 
    }

    public void Clean()
    {
        data = null;
        StackSize = 0;
        UpdateSlot(); 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !IsEmpty)
        {
            dragDropHandler.slotDraggedFrom = this;
            dragDropHandler.isDragging = true;
            //Debug.Log($"Started dragging from slot: {this.gameObject.name}, IsEmpty: {IsEmpty}");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (dragDropHandler.isDragging)
        {
            //Debug.Log($"Pointer Up: Dragging from {dragDropHandler.slotDraggedFrom.gameObject.name}");
        
            if (dragDropHandler.slotDraggedTo == null)
            {
                Drop();
                //Debug.Log("Dropped item.");
                dragDropHandler.isDragging = false;
            }   
            else
            {
                inventory.DragDrop(dragDropHandler.slotDraggedFrom, dragDropHandler.slotDraggedTo);
                //Debug.Log($"Dragged to {dragDropHandler.slotDraggedTo.gameObject.name}");
                dragDropHandler.isDragging = false;
            }

        dragDropHandler.slotDraggedFrom = null;
        dragDropHandler.slotDraggedTo = null;
        }
    }

    public void Try_Use()
    {
        if (data == null)
        {
            return;
        }

        if (data.itemtype == ItemScriptableObject.ItemType.Weapon || data.itemtype == ItemScriptableObject.ItemType.MeleeWeapon)
        {
            bool ShouldJustUnequip = false;


            //UNEQUIP EVERY ACTIVE WEAPON
            for (int i = 0; i < inventory.weapons.Length; i++)
            {
                if (inventory.weapons[i].gameObject.activeSelf)
                {
                    if (inventory.weapons[i].slotEquippedOn == this)
                    {
                        ShouldJustUnequip = true;
                    }


                    inventory.weapons[i].Unequip();
                }
            }

            if(ShouldJustUnequip)
            {
                return;
            }

            //EQUIP WEAPON
            for (int i = 0; i < inventory.weapons.Length; i++)
            {
                if(inventory.weapons[i].weaponData == data)
                {
                    inventory.weapons[i].Equip(this);
                }
            }
        }


        if (data.itemtype == ItemScriptableObject.ItemType.Consumable)
        {
            Consume();
        }

        if (data.itemtype == ItemScriptableObject.ItemType.Buildable)
        {
            Try_Build();
        }
    }

    public void Try_Build()
    {
        //UNEQUIP EVERY ACTIVE WEAPON
            for (int i = 0; i < inventory.weapons.Length; i++)
            {
                if (inventory.weapons[i].gameObject.activeSelf)
                {
                    inventory.weapons[i].Unequip();
                }
            }

        if (inventory.building == null)
        {
            inventory.building = GetComponentInParent<PlayerMovement>().GetComponentInChildren<BuildingHandler>();
        }
        if (inventory.building.slotInUse == null)
        {
            inventory.building.slotInUse = this;
        }
        else
        {
            if (inventory.building.slotInUse == this)
            {
                inventory.building.slotInUse = null;
            }
            else
            {
                inventory.building.slotInUse = this;


                Destroy(inventory.building.ghost.gameObject);
            }
        }
    }

    public void Consume()
    {
        PlayerStats stats = GetComponentInParent<PlayerStats>();
        stats.health += data.healthChange;
        stats.hunger += data.hungerChange;
        stats.thirst += data.thirstChange;
        stats.armor += data.armorChange;

        StackSize--;
        UpdateSlot();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dragDropHandler.isDragging)
        {
            dragDropHandler.slotDraggedTo = this;
            //Debug.Log($"Dragging over slot: {this.gameObject.name}");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (dragDropHandler.isDragging)
        {
            dragDropHandler.slotDraggedTo = null;
            //Debug.Log("Item follow cursor");
        }
    }
}