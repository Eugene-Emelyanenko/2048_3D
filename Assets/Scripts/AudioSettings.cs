using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Default Settings")]
    [Range(0f, 1f)][SerializeField] private float defaultMasterValue = 1f;
    [Range(0f, 1f)][SerializeField] private float defaultMusicValue = 0.75f;
    [Range(0f, 1f)][SerializeField] private float defaultSfxValue = 0.75f;

    private void Start()
    {
        LoadSettings();

        masterSlider.onValueChanged.AddListener(OnMasterVolumeChange);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        sfxSlider.onValueChanged.AddListener(OnSfxVolumeChange);
    }

    public void OnMasterVolumeChange(float value)
    {
        SetVolume("Master", value);
        PlayerPrefs.SetFloat("Master", value);
    }

    public void OnMusicVolumeChange(float value)
    {
        SetVolume("Music", value);
        PlayerPrefs.SetFloat("Music", value);
    }

    public void OnSfxVolumeChange(float value)
    {
        SetVolume("Sfx", value);
        PlayerPrefs.SetFloat("Sfx", value);
    }

    private void SetVolume(string parameter, float value)
    {
        float dB = value <= 0.0001f ? -80f : Mathf.Log10(value) * 20f;
        audioMixer.SetFloat(parameter, dB);
    }

    private void LoadSettings()
    {
        float masterValue = PlayerPrefs.GetFloat("Master", defaultMasterValue);
        float musicValue = PlayerPrefs.GetFloat("Music", defaultMusicValue);
        float sfxValue = PlayerPrefs.GetFloat("Sfx", defaultSfxValue);

        SetVolume("Master", masterValue);
        SetVolume("Music", musicValue);
        SetVolume("Sfx", sfxValue);

        masterSlider.value = masterValue;
        musicSlider.value = musicValue;
        sfxSlider.value = sfxValue;
    }
}
