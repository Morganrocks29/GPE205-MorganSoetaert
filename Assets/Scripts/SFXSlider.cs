using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    [SerializeField] Slider sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = sfxSlider.value;
    }

    private void Load()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }
}
