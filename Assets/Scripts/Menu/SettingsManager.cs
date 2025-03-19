using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set;}
    public float mouseSensitivity = 10f;
    public float sensitivityMultiplier = 5f;
    public AudioSource animalAudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAnimalSound(AudioClip clip, Vector3 position)
    {
        if (clip == null || animalAudioSource == null) 
        {
            return;
        }
        
        animalAudioSource.transform.position = position;
        animalAudioSource.PlayOneShot(clip);
    }

    public void SetAnimalVolume(float volume)
    {
        if (animalAudioSource != null)
        {
            animalAudioSource.volume = volume;
        }
    }
}
