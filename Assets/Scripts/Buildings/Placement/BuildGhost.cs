using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGhost : MonoBehaviour
{
    public GameObject buildPrefab;
    public bool canBuild;
    private int collisionCount = 0;

    private void Start()
    {
        canBuild = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(!other.isTrigger)
        {
            collisionCount++;
            canBuild = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            collisionCount--;
            canBuild = true;
        }
    }
}
