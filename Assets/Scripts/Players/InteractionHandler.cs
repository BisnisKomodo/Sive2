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
                    }
                }
                if (craftingStation != null)
                {
                    if (craftingStation.opened)
                    {
                        craftingStation.Close(); 
                    }
                    else
                    {
                        craftingStation.Open();
                    }
                }
            }

            if (pickup != null || storage != null || craftingStation != null)
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
