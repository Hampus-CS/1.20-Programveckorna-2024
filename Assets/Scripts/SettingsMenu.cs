using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer gameAudioMixer;
    private const string VolumePrefsKey = "Volume";
    private const string GameVolumePrefsKey = "GameVolume";

    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        float savedVolume = PlayerPrefs.GetFloat(VolumePrefsKey, 1.0f);
        float savedGameVolume = PlayerPrefs.GetFloat(GameVolumePrefsKey, 1.0f);
        SetVolume(savedVolume);
        SetGameVolume(savedGameVolume);

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("volume", volume);
        }
        PlayerPrefs.SetFloat(VolumePrefsKey, volume);
    }


    public void SetGameVolume(float gameVolume)
    {
        if (gameAudioMixer != null)
        {
            gameAudioMixer.SetFloat("GameVolume", gameVolume);
        }
        PlayerPrefs.SetFloat(GameVolumePrefsKey, gameVolume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
