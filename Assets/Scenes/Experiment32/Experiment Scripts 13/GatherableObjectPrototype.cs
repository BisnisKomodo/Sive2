using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherableObjectPrototype : MonoBehaviour
{
    public enum DeathType { Destroy, EnablePhysics };
    public DeathType deathType;
    public GatherDataScriptableObject[] gatherData;
    public int hits;
    public ItemScriptableObject[] prefferedtools;
    public int toolMultiplier = 1;

    private Rigidbody rb; // Cache Rigidbody reference
    private bool hasDied;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Gather(ItemScriptableObject toolUsed, InventoryManager inventory)
    {
        if (hits <= 0 || hasDied)
        {
            return;
        }

        bool usingRightTool = false;

        // Check for tools
        if (prefferedtools.Length > 0)
        {
            foreach (var tool in prefferedtools)
            {
                if (tool == toolUsed)
                {
                    usingRightTool = true;
                    break;
                }
            }
        }

        // Select gather data
        int selectedGatherData = Random.Range(0, gatherData.Length);

        // Apply tool multiplier
        int gatherAmount = gatherData[selectedGatherData].amount;
        if (usingRightTool)
        {
            gatherAmount *= toolMultiplier;
        }

        // Add gathered items to inventory
        inventory.AddItem(gatherData[selectedGatherData].item, gatherAmount);

        // Reduce hits and check for death
        hits--;

        if (hits <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        hasDied = true;

        if (deathType == DeathType.Destroy)
        {
            Destroy(gameObject);
        }
        else if (deathType == DeathType.EnablePhysics && rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddTorque(Vector3.right * 25);
            Destroy(gameObject, 15f);
        }
    }
}
