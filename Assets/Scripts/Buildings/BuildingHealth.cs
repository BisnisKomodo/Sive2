using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    public float buildingHealth = 100f;

    public void TakeDamage(float damageAmount)
    {
        buildingHealth -= damageAmount;
        if (buildingHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
