using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherableObject : MonoBehaviour
{
    
    public enum DeathType { Destroy, EnablePhysics};
    public DeathType deathType;
    public GatherDataScriptableObject[] gatherData;
    public int hits;
    public ItemScriptableObject[] prefferedtools;
    public int toolMultiplier = 1;

    bool hasDied;

    private void Update()
    {
        if (hits <= 0 && !hasDied)
        {
            hasDied = true;
            if (deathType == DeathType.Destroy)
            {
                Destroy(gameObject);
            }
            else if (deathType == DeathType.EnablePhysics)
            {
                if (GetComponent<Rigidbody>() != null)
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    GetComponent<Rigidbody>().useGravity = true;

                    GetComponent<Rigidbody>().AddTorque(Vector3.right * 20);

                    Destroy(gameObject, 15f);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            hasDied = true;
        }
    }







    public void Gather(ItemScriptableObject toolUsed, InventoryManager inventory)
    {

        if (hits <= 0)
        {
            return;
        }
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
