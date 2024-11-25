using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
//     public Transform sun;

//     [Header("Cycle Setting")]
//     public float timeOfDay = 0; //Current time in the cycle
//     public float cycleDuration = 300; //Total duration cycle
//     public float dayStartTime = 0;
//     public float dayEndTime = 180;
//     [Space]
//     public float cycleSpeed = 1f; //cycle speed progress

//     [Header("Lighting Settings")]
//     public float dayTimeSunIntensity = 1f;
//     public float nightTimeSunIntensity = 0f;
//     [Space]
//     public float dayTimeAmbientIntensity = 1;
//     public float nightTimeAmbientIntensity = 0.15f;
//     [Space]
//     public float intensityChangeSpeed = 1f;
//     public Material skyBox;
//     public Color dayTimeColor;
//     public Color nightTimeColor;

    
//     [HideInInspector] public bool isNightTime;

//     private void Start()
//     {
//         isNightTime = false; // Ensure starting with daytime
//         sun.GetComponentInChildren<Light>().intensity = dayTimeSunIntensity;
//         if (!isNightTime)
//         {
//             sun.GetComponentInChildren<Light>().intensity = dayTimeSunIntensity;
//         }
//         else
//         {
//             sun.GetComponentInChildren<Light>().intensity = nightTimeSunIntensity;
//         }
//     }

//     private void Update()
//     {
//         if (!isNightTime)
//         {
//             sun.GetComponentInChildren<Light>().intensity = Mathf.Lerp(sun.GetComponentInChildren<Light>().intensity, dayTimeSunIntensity, intensityChangeSpeed * Time.deltaTime);
//             RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, dayTimeAmbientIntensity, intensityChangeSpeed * Time.deltaTime);
//             if (skyBox != null)
//             {
//                 RenderSettings.skybox.SetColor("Color_", Color.Lerp(skyBox.color, dayTimeColor, intensityChangeSpeed * Time.deltaTime));
//                 //skyBox.color = Color.Lerp(skyBox.color, dayTimeColor, intensityChangeSpeed * Time.deltaTime);
//             }
//         }
//         else
//         {
//             sun.GetComponentInChildren<Light>().intensity = Mathf.Lerp(sun.GetComponentInChildren<Light>().intensity, nightTimeSunIntensity, intensityChangeSpeed * Time.deltaTime);
//             RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, nightTimeAmbientIntensity, intensityChangeSpeed * Time.deltaTime);
            
//             if (skyBox != null)
//             {
//                 RenderSettings.skybox.SetColor("Color_", Color.Lerp(skyBox.color, nightTimeColor, intensityChangeSpeed * Time.deltaTime));
//                 //skyBox.color = Color.Lerp(skyBox.color, nightTimeColor, intensityChangeSpeed * Time.deltaTime);
//             }
//         }

//         if (timeOfDay > cycleDuration)
//         {
//             timeOfDay = 0;
//         }

//         timeOfDay += cycleSpeed * Time.deltaTime;

//         if (timeOfDay > dayStartTime && timeOfDay < dayEndTime)
//         {
//             timeOfDay += cycleSpeed * Time.deltaTime;
//         }
//         // else
//         // {
//         //     timeOfDay += cycleSpeed * Time.deltaTime;
//         // }

//         UpdateLighting();
//     }

//     public void UpdateLighting()
//     {
//         sun.localRotation = Quaternion.Euler((timeOfDay * 360 / cycleDuration), 0 , 0);

//         if (timeOfDay > dayStartTime || timeOfDay > dayStartTime)
//         {
//             isNightTime = false;
//         }
//         else
//         {
//             isNightTime = true;
//         }
//     }
// }
    public Transform sun;

    [Header("Cycle Settings")]
    public float sunStartingPoint = 100;
    public float timeOfDay = 0; // Current time in the cycle
    public float cycleDuration = 300; // Total duration of the cycle in seconds
    public float dayStartTime = 0; // Start of the day (in seconds)
    public float dayEndTime = 180; // End of the day (in seconds)
    public float cycleSpeed = 1f; // Speed multiplier for the cycle
    public float sunSpeed = 1f;

    [Header("Lighting Settings")]
    public float dayTimeSunIntensity = 1f;
    public float nightTimeSunIntensity = 0f;
    public float dayTimeAmbientIntensity = 1f;
    public float nightTimeAmbientIntensity = 0.15f;
    public float intensityChangeSpeed = 1f;

    [Header("Skybox Settings")]
    public float daySkyboxExposure = 1.3f; // Brighter sky during the day
    public float nightSkyboxExposure = 0.3f; // Dimmer sky during the night

    [HideInInspector] public bool isNightTime;

    private Light sunLight;

    private void Start()
    {
        sunLight = sun.GetComponentInChildren<Light>();

        // // Initialize lighting and skybox exposure based on the initial time of day
        // if (timeOfDay >= dayStartTime && timeOfDay <= dayEndTime)
        // {
        //     isNightTime = false;
        //     RenderSettings.skybox.SetFloat("_Exposure", daySkyboxExposure);
        // }
        // else
        // {
        //     isNightTime = true;
        //     RenderSettings.skybox.SetFloat("_Exposure", nightSkyboxExposure);
        // }
        sunStartingPoint = Math.Clamp(sunStartingPoint, 0, 360);
        timeOfDay = Mathf.Clamp(timeOfDay, 0, 360);

        UpdateLighting();
    }

    private void Update()
    {
        // Increment time of day
        timeOfDay += (cycleSpeed / cycleDuration) * 360 * Time.deltaTime;
        if (timeOfDay >= 360f)
        {
            timeOfDay -= 360f; // Loop the cycle back to 0
        }

        // Skybox Controller here
        sunStartingPoint += (sunSpeed / cycleDuration) * 360 * Time.deltaTime;
        if (sunStartingPoint >= 360f)
        {
            sunStartingPoint -= 360f; // Loop the cycle back to 0
        }

        // Determine if it's night time
        // if (timeOfDay >= dayStartTime && timeOfDay <= dayEndTime)
        // {
        //     isNightTime = false;
        // }
        // else
        // {
        //     isNightTime = true;
        // }

        isNightTime = timeOfDay < dayStartTime || timeOfDay > dayEndTime;

        // Update lighting and rotation
        UpdateLighting();
    }

    private void UpdateLighting()
    {
        // Rotate the sun
        sun.localRotation = Quaternion.Euler(new Vector3(sunStartingPoint, 0, 0));

        // Update light intensity
        if (isNightTime)
        {
            sunLight.intensity = Mathf.Lerp(sunLight.intensity, nightTimeSunIntensity, intensityChangeSpeed * Time.deltaTime);
            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, nightTimeAmbientIntensity, intensityChangeSpeed * Time.deltaTime);

            // Update skybox exposure for night
            if (RenderSettings.skybox.HasProperty("_Exposure"))
            {
                RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(RenderSettings.skybox.GetFloat("_Exposure"), nightSkyboxExposure, intensityChangeSpeed * Time.deltaTime));
            }
        }
        else
        {
            sunLight.intensity = Mathf.Lerp(sunLight.intensity, dayTimeSunIntensity, intensityChangeSpeed * Time.deltaTime);
            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, dayTimeAmbientIntensity, intensityChangeSpeed * Time.deltaTime);

            // Update skybox exposure for day
            if (RenderSettings.skybox.HasProperty("_Exposure"))
            {
                RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(RenderSettings.skybox.GetFloat("_Exposure"), daySkyboxExposure, intensityChangeSpeed * Time.deltaTime));
            }
        }
    }
}

