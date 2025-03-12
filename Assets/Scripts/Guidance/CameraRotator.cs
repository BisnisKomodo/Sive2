using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public Camera mainCamera;
    public float rotationAmount = 90f; 
    public float rotationSpeed = 5f; 

    private Quaternion targetRotation;
    private bool isRotating = false;
    public GameObject disabler;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main; 

        targetRotation = mainCamera.transform.rotation;
    }

    private void Update()
    {
        if (isRotating)
        {
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(mainCamera.transform.rotation, targetRotation) < 0.1f)
            {
                mainCamera.transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }

    public void RotateCamera()
    {
        if (!isRotating)
        {
            targetRotation *= Quaternion.Euler(0, rotationAmount, 0); 
            isRotating = true;
        }
        disabler.gameObject.SetActive(false);
    }
}
