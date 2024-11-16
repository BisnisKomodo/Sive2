using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePrototype : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float damage = 20;
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.name == "Player")
        {
            target.GetComponent<PlayerStats>().health -= damage;
        }
    }
}
