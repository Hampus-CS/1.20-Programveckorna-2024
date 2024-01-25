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
    private const string GameScreenShakePrefsKey = "ScreenShake";
    public float settingScreenShake = 0f;

    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {

        // The game and menu volume.
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefsKey, 1.0f);
        float savedGameVolume = PlayerPrefs.GetFloat(GameVolumePrefsKey, 1.0f);
        float savedScreenShake = PlayerPrefs.GetFloat(GameScreenShakePrefsKey, 0.25f);
        SetVolume(savedVolume);
        SetGameVolume(savedGameVolume);
        SetScreenShake(savedScreenShake);

        // Screens resolution.
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

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

    public void SetScreenShake(float screeners_of_the_shakers)
    {
        PlayerPrefs.SetFloat(GameScreenShakePrefsKey, screeners_of_the_shakers);
        settingScreenShake = screeners_of_the_shakers;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
