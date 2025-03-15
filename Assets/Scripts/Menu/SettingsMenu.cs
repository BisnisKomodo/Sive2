using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Dropdown graphicsQuality;
    public Slider fieldOfView;
    public Toggle postProcessing;
    public Slider sensitivitySlider;
    public Slider multiplierSlider;
    public Gamemenu gameMenu;
    public Slider audioSlider;
    public AudioMixer audioMixer;
    private void Start() 
    {
        graphicsQuality.value = (int)Settings.graphicsQuality;
        fieldOfView.value = Settings.fov;
        postProcessing.isOn = Settings.postProcessing;
        sensitivitySlider.value = SettingsManager.Instance.mouseSensitivity;
        multiplierSlider.value = SettingsManager.Instance.sensitivityMultiplier;
        audioSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1);

        ApplyChanges();
        audioSlider.onValueChanged.AddListener(SetAudioVolume);
    }
    private void Update() 
    {
        Settings.graphicsQuality = (Settings.GraphicsQuality)graphicsQuality.value;
        Settings.fov = fieldOfView.value;
        Settings.postProcessing = postProcessing.isOn;
        Settings.mouseSensitivity = sensitivitySlider.value;

        SettingsManager.Instance.mouseSensitivity = sensitivitySlider.value;
        SettingsManager.Instance.sensitivityMultiplier = multiplierSlider.value;
    }
    
    public void SetAudioVolume(float volume)
    {
        float volumeDB = Mathf.Log10(volume) * 20; 
        audioMixer.SetFloat("MasterVolume", volumeDB);

        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }
    public void Close()
    {
        gameObject.SetActive(false);
        gameMenu.continueButton.SetActive(true);
        gameMenu.backToMainMenuButton.SetActive(true);
        gameMenu.settingButton.SetActive(true);
    }

    public void ApplyChanges()
    {
        QualitySettings.SetQualityLevel((int)Settings.graphicsQuality);
    }
}
