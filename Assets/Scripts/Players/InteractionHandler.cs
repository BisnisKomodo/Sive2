using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHandler : MonoBehaviour
{
    public LayerMask interactableLayers;
    public float interactionRange = 2f;
    public KeyCode interactionKey = KeyCode.E;
    public Text interactionText;

    private PlayerMovement playerMovement;

    // private void Start()
    // {
    //     playerMovement = GetComponentInParent<PlayerMovement>();
    // }

    private void Update()
    {
        Interact();
        
    }

    private void Interact()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, interactableLayers))
        {
            Pickup pickup = hit.transform.GetComponent<Pickup>();
            Storage storage = hit.transform.GetComponent<Storage>();
            CraftingStation craftingStation = hit.transform.GetComponent<CraftingStation>();
            ArmoryStation armoryStation = hit.transform.GetComponent<ArmoryStation>();
            SimpleBenchCraft simpleBenchCraft = hit.transform.GetComponent<SimpleBenchCraft>();

            if (Input.GetKeyDown(interactionKey))
            {
                if (pickup != null)
                {
                    GetComponentInParent<WindowHandler>().inventory.AddItem(pickup);
                }
                if (storage != null)
                {
                    if (!storage.opened)
                    {
                        GetComponentInParent<WindowHandler>().inventory.opened = true;

                        storage.Open(GetComponentInParent<WindowHandler>().storage);

                        //playerMovement.isInteracting = true;
                    }
                }

                if (craftingStation != null)
                {
                    if (craftingStation.opened)
                    {
                        craftingStation.Close();
                        //playerMovement.isInteracting = false; 
                    }
                    else
                    {
                        craftingStation.Open();
                        //playerMovement.isInteracting = true;
                    }
                }
                if (armoryStation != null)
                {
                    if (armoryStation.opened)
                    {
                        armoryStation.Close();
                        //playerMovement.isInteracting = false; 
                    }
                    else
                    {
                        armoryStation.Open();
                        //playerMovement.isInteracting = true;
                    }
                }
                if (simpleBenchCraft != null)
                {
                    if (simpleBenchCraft.opened)
                    {
                        simpleBenchCraft.Close();
                    }
                    else    
                    {
                        simpleBenchCraft.Open();
                    }
                }
            }

            if (pickup != null || storage != null || craftingStation != null || armoryStation != null || simpleBenchCraft != null)
            {
                interactionText.gameObject.SetActive(true);

                if (pickup != null)
                {
                    interactionText.text = $"{pickup.data.ItemName} x{pickup.StackSize}";
                }

                if (storage != null)
                {
                    interactionText.text = $"Open";
                }

                if (craftingStation != null)
                {
                    interactionText.text = $"Open";
                }

                if (armoryStation != null)
                {
                    interactionText.text = $"Open";
                }

                if (simpleBenchCraft != null)
                {
                    interactionText.text = $"Open";
                }
            }
            else
            {
                interactionText.gameObject.SetActive(false);
            }
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}
