using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathtouch : MonoBehaviour
{
    private float damage = 200f;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.health -= damage;
            }
        }
    }
}
