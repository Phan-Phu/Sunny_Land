using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider volumeSFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = PlayerPrefsController.GetMasterVolume();
        volumeSFXSlider.value = PlayerPrefsController.GetSFXVolume();
    }

    // Update is called once per frame
    void Update()
    {
        SetMusicPlayer();
    }

    private void SetMusicPlayer()
    {
        var musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }
        else
        {
            Debug.LogWarning("No music player found...");
        }
    }

    public void FullScreenOrWindow(Toggle check)
    {
        if (check.isOn == true)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.SetResolution(1280, 720, false);
        }
    }

    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        PlayerPrefsController.SetSFXVolume(volumeSFXSlider.value);
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }
}
