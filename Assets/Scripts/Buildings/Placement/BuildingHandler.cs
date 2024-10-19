using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    public Slot slotInUse;
    public Transform offGroundPoint;
    [Space]
    public float range = 4f;
    public Color allowed;
    public Color blocked;
    [Space]
    public BuildGhost ghost;
    public bool canBuild;

    private void Update()
    {
        UpdateBuilding();
    }

    public void UpdateColors()
    {
        MeshRenderer renderer = null;

        if (ghost.GetComponent<MeshRenderer>() != null)
        {
            renderer = ghost.GetComponent<MeshRenderer>();
        }
        else if (ghost.GetComponentInChildren<MeshRenderer>() != null)
        {
            renderer = ghost.GetComponentInChildren<MeshRenderer>();
        }


        if (renderer.materials.Length > 1)
        {
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                if (canBuild && ghost.canBuild)
                {
                    renderer.materials[i].color = allowed;
                }
                else
                {
                    renderer.materials[i].color = blocked;
                }
            }

        }
        else if (renderer.materials.Length == 1)
        {
            if (canBuild && ghost.canBuild)
            {
                renderer.material.color = allowed;
            }
            else
            {
                renderer.material.color = blocked;
            }
        }
    }

    public void UpdateBuilding()
    {
        // Debug log for slotInUse
        if (slotInUse == null)
        {
            if (ghost != null)
            {
                Destroy(ghost.gameObject);
                ghost = null; // Resetting the ghost reference
            }
            return;
        }

        // Debug log for slotInUse data
        Debug.Log($"Slot in use: {slotInUse.data?.name}, Stack Size: {slotInUse.StackSize}");

        if (slotInUse.StackSize <= 0 || slotInUse.data == null)
        {
            Debug.Log("Slot is empty or invalid.");
            if (ghost != null)
            {
                Destroy(ghost.gameObject);
                ghost = null; // Resetting the ghost reference
            }
            return;
        }

        if (ghost == null)
        {
            ghost = Instantiate(slotInUse.data.ghost, offGroundPoint.transform.position, GetComponentInParent<PlayerMovement>().transform.rotation);
            Debug.Log("Ghost instantiated.");
        }

        UpdateColors();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log($"Raycast hit: {hit.transform.name}");
            if (hit.transform.GetComponent<BuildBlocked>() == null)
            {
                ghost.transform.position = hit.point;
                ghost.transform.rotation = GetComponentInParent<PlayerMovement>().transform.rotation;
                canBuild = true;
                Debug.Log("Building is allowed.");
            }
            else
            {
                ghost.transform.position = offGroundPoint.position;
                ghost.transform.rotation = GetComponentInParent<PlayerMovement>().transform.rotation;
                canBuild = false;
                Debug.Log("Building is blocked.");
            }
        }
        else
        {
            ghost.transform.position = offGroundPoint.position;
            ghost.transform.rotation = GetComponentInParent<PlayerMovement>().transform.rotation;
            canBuild = false;
            Debug.Log("No hit detected.");
        }

        if (Input.GetButtonDown("Fire1") && canBuild && ghost.canBuild)
        {
            slotInUse.StackSize--;
            slotInUse.UpdateSlot();
            Instantiate(ghost.buildPrefab, ghost.transform.position, ghost.transform.rotation);
            Debug.Log($"Building placed. Remaining stack size: {slotInUse.StackSize}");
        }
    }
}
