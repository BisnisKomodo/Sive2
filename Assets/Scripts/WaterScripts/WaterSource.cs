// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WaterSource : MonoBehaviour
// {
//     public ItemScriptableObject bowlItem;
//     public ItemScriptableObject seaWaterBowlItem;

//     private void OnTriggerEnter(Collider other) 
//     {
//         //Debug.Log("OnTriggerEnter called");
//         if (other.CompareTag("Player"))
//         {
//             //Debug.Log("Player entered trigger");
//             InventoryManager inventoryManager = other.GetComponentInChildren<InventoryManager>();
//             if (inventoryManager == null)
//             {
//                 //Debug.LogError("No InventoryManager Found On The Player");
//                 return;
//             }

//             //Debug.Log("InventoryManager found");

//             Slot bowlSlot = FindBowlInInventory(inventoryManager);

//             if (bowlSlot != null)
//             {
//                 //Debug.Log("Empty Bowl found in inventory");
//                 RemoveBowlFromInventory(bowlSlot);
//                 AddSeaWaterToInventory(inventoryManager);
//             }
//             //else
//             //{
//                 //Debug.Log("No Empty bowl found in inventory");
//             //}
//         }
//     }

//     private Slot FindBowlInInventory(InventoryManager inventoryManager)
//     {
//         //Debug.Log("Search for a freaking bowl!");
//         foreach(Slot slot in inventoryManager.inventorySlots)
//         {
//             if(!slot.IsEmpty && slot.data == bowlItem)
//             {
//                 //Debug.Log("Bowl founded!");
//                 return slot;
//             }
//         }
//         return null;
//     }

//     private void RemoveBowlFromInventory(Slot bowlSlot)
//     {
//         bowlSlot.Clean();
//         bowlSlot.UpdateSlot();
//     }

//     private void AddSeaWaterToInventory(InventoryManager inventoryManager)
//     {
//         foreach (Slot slot in inventoryManager.inventorySlots)
//         {
//             if (slot.IsEmpty)
//             {
//                 slot.AddItemToSlot(seaWaterBowlItem, 1);
//                 slot.UpdateSlot();
//                 return;
//             }
//         }
//         //Debug.LogWarning("No Empty Slot Available for the sea water bowl");
//     }
// }

// using UnityEngine;

// public class WaterSource : MonoBehaviour
// {
//     public ItemScriptableObject bowlItem;
//     public ItemScriptableObject dirtyWaterBowlItem;

//     private void OnTriggerEnter(Collider other) 
//     {
//         if (other.CompareTag("Player"))
//         {
//             InventoryManager inventoryManager = other.GetComponentInChildren<InventoryManager>();
//             if (inventoryManager == null)
//             {
//                 Debug.LogError("No InventoryManager Found On The Player or its children.");
//                 return;
//             }

//             int bowlsReplaced = ReplaceBowlWithDirtyWater(inventoryManager);

//             if (bowlsReplaced > 0)
//             {
//                 Debug.Log($"{bowlsReplaced} bowl(s) filled with seawater.");
//             }
//             else
//             {
//                 Debug.Log("No empty bowl found in inventory.");
//             }
//         }
//     }

//     private int ReplaceBowlWithDirtyWater(InventoryManager inventoryManager)
//     {
//         int bowlsReplaced = 0;

//         foreach (Slot slot in inventoryManager.inventorySlots)
//         {
//             Debug.Log($"Checking slot: {slot}");

//             if (!slot.IsEmpty && slot.data == bowlItem)
//             {
//                 Debug.Log($"Found empty bowl in slot: {slot}");
//                 slot.Clean();
//                 slot.AddItemToSlot(dirtyWaterBowlItem, 1);
//                 slot.UpdateSlot();
//                 bowlsReplaced++;
//             }
//             else
//             {
//                 Debug.Log($"Slot empty or not a bowl: {slot}");
//             }
//         }

//         return bowlsReplaced;
//     }
    
// }

using UnityEngine;

public class WaterSource : MonoBehaviour
{
    public ItemScriptableObject bowlItem;
    public ItemScriptableObject seaWaterBowlItem;
    public AudioSource audioSourcez;
    public AudioClip waterSound;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            
            InventoryManager inventoryManager = other.GetComponentInChildren<InventoryManager>();
            if (inventoryManager == null)
            {
                //Debug.LogError("No InventoryManager Found On The Player or its children.");
                return;
            }

            int bowlsReplaced = ReplaceBowlsWithSeaWater(inventoryManager);

            if (bowlsReplaced > 0)
            {
                if (audioSourcez != null && waterSound != null)
                {
                    audioSourcez.PlayOneShot(waterSound);
                }
            }
        }
    }

    private int ReplaceBowlsWithSeaWater(InventoryManager inventoryManager)
    {
        int bowlsReplaced = 0;

        foreach (Slot slot in inventoryManager.inventorySlots)
        {
            //Debug.Log($"Checking slot: {slot} - Stack size: {slot.StackSize}");
            
            if (!slot.IsEmpty && slot.data == bowlItem)
            {
                //Debug.Log($"Found empty bowl in slot: {slot}");

                //Check if the slot has more than 1 bowl and perform the action accordingly
                int stackSize = slot.StackSize;
                for (int i = 0; i < stackSize; i++)
                {
                    // Replace each bowl with seawater
                    slot.Clean();
                    slot.AddItemToSlot(seaWaterBowlItem, stackSize);
                    slot.UpdateSlot();
                    bowlsReplaced++;
                }
            }
            else
            {
                //Debug.Log($"Slot empty or not a bowl: {slot}");
            }
        }

        return bowlsReplaced;
    }
}
