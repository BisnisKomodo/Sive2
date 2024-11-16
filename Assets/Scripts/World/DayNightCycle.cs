using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Transform sun;

    [Header("Cycle Setting")]
    public float timeOfDay = 180; //Current time in the cycle
    public float cycleDuration = 300; //Total duration cycle
    public float dayStartTime = 100;
    public float dayEndTime = 250;
    [Space]
    public float cycleSpeed = 1f; //cycle speed progress

    [Header("Lighting Settings")]
    public float dayTimeSunIntensity = 1f;
    public float nightTimeSunIntensity = 0f;
    [Space]
    public float dayTimeAmbientIntensity = 1;
    public float nightTimeAmbientIntensity = 0.15f;
    [Space]
    public float intensityChangeSpeed = 1f;
    public Material skyBox;
    public Color dayTimeColor;
    public Color nightTimeColor;

    
    [HideInInspector] public bool isNightTime;

    private void Start()
    {
        if (!isNightTime)
        {
            sun.GetComponentInChildren<Light>().intensity = dayTimeSunIntensity;
        }
        else
        {
            sun.GetComponentInChildren<Light>().intensity = nightTimeSunIntensity;
        }
    }

    private void Update()
    {
        if (!isNightTime)
        {
            sun.GetComponentInChildren<Light>().intensity = Mathf.Lerp(sun.GetComponentInChildren<Light>().intensity, dayTimeSunIntensity, intensityChangeSpeed * Time.deltaTime);
            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, dayTimeAmbientIntensity, intensityChangeSpeed * Time.deltaTime);
            if (skyBox != null)
            {
                RenderSettings.skybox.SetColor("Color_", Color.Lerp(skyBox.color, dayTimeColor, intensityChangeSpeed * Time.deltaTime));
                //skyBox.color = Color.Lerp(skyBox.color, dayTimeColor, intensityChangeSpeed * Time.deltaTime);
            }
        }
        else
        {
            sun.GetComponentInChildren<Light>().intensity = Mathf.Lerp(sun.GetComponentInChildren<Light>().intensity, nightTimeSunIntensity, intensityChangeSpeed * Time.deltaTime);
            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, nightTimeAmbientIntensity, intensityChangeSpeed * Time.deltaTime);
            
            if (skyBox != null)
            {
                RenderSettings.skybox.SetColor("Color_", Color.Lerp(skyBox.color, nightTimeColor, intensityChangeSpeed * Time.deltaTime));
                //skyBox.color = Color.Lerp(skyBox.color, nightTimeColor, intensityChangeSpeed * Time.deltaTime);
            }
        }

        if (timeOfDay > cycleDuration)
        {
            timeOfDay = 0;
        }

        timeOfDay += cycleSpeed * Time.deltaTime;

        if (timeOfDay > dayStartTime && timeOfDay < dayEndTime)
        {
            timeOfDay += cycleSpeed * Time.deltaTime;
        }
        else
        {
            timeOfDay += (cycleSpeed * 2) * Time.deltaTime;
        }

        UpdateLighting();
    }

    public void UpdateLighting()
    {
        sun.localRotation = Quaternion.Euler((timeOfDay * 360 / cycleDuration), 0 , 0);

        if (timeOfDay > dayStartTime || timeOfDay > dayStartTime)
        {
            isNightTime = true;
        }
        else
        {
            isNightTime = false;
        }
    }
}
