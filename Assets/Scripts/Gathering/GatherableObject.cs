using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherableObject : MonoBehaviour
{
   public GatherDataScriptableObject[] gatherData;
   public int hits;
   public ItemScriptableObject[] prefferedtools;
   public int toolMultiplier = 1;

   private void Update()
   {
        if (hits <= 0)
        {
            Destroy(gameObject);
        }
   }







   public void Gather(ItemScriptableObject toolUsed, InventoryManager inventory)
   {
        bool usingRightTool = false;

        //CHECK FOR TOOLS
        if(prefferedtools.Length > 0)
        {
            for (int i = 0; i < prefferedtools.Length; i++)
            {
                if (prefferedtools[i] == toolUsed)
                {
                    usingRightTool = true;
                    break;
                }
            }
        }


        //Gather
        int selectedGatherData = Random.Range(0, gatherData.Length);
        inventory.AddItem(gatherData[selectedGatherData].item, gatherData[selectedGatherData].amount);

        hits--;
   }
}
