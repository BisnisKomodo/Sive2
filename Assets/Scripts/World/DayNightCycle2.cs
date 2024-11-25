using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle2 : MonoBehaviour
{
    public float rotationSpeed = 10f;

    private void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
