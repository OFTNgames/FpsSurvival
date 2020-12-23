using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MasterVolumeScript : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider masterVolume;
    public Slider sfxVolume;
    public Slider musicVolume;

    string master = "MasterVolumeSetting";
    string music = "MusicVolumeSetting";
    string sfx = "SfxVolumeSetting";

    private void Start()
    {
        masterVolume.value = PlayerPrefs.GetFloat(master);
        sfxVolume.value = PlayerPrefs.GetFloat(sfx);
        musicVolume.value = PlayerPrefs.GetFloat(music);
    }
    
    public void SetSoundMaster(float soundLevel)
    {
        masterMixer.SetFloat("MasterVolume", soundLevel);
        PlayerPrefs.SetFloat(master, soundLevel);
        PlayerPrefs.Save();
    }

    public void SetSoundMusic(float soundLevel)
    {
        masterMixer.SetFloat("MusicVolume", soundLevel);
        PlayerPrefs.SetFloat(music, soundLevel);
        PlayerPrefs.Save();
    }
    public void SetSoundSFX(float soundLevel)
    {
        masterMixer.SetFloat("SFXVolume", soundLevel);
        PlayerPrefs.SetFloat(sfx, soundLevel);
        PlayerPrefs.Save();
    }
}
