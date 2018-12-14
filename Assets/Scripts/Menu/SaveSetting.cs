using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSetting : MonoBehaviour
{
    // UI variables
    public Slider musicVolume;
    public Slider sfxVolume;
    public Toggle randomMap;
    public Toggle mapOfDay;
    public Toggle seededMap;
    public Toggle isMultiEnabled;
    public Text seedNumber;
    public int number;

    public void saveSettings()
    {
        // Music Volume
        GameManager.instance.musicVol = musicVolume.value;
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);

        // Sound Effects Volume
        GameManager.instance.sfxVol = sfxVolume.value;
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume.value);

        // Multiplayer Settings
        GameManager.instance.isMultiplayer = isMultiEnabled.isOn;
        if (isMultiEnabled.isOn == false)
        {
            PlayerPrefs.SetInt("isMultiplayer", 0);
        }
        else
        {
            PlayerPrefs.SetInt("isMultiplayer", 1);
        }

        // Seed Number Settings
        number = int.Parse(seedNumber.text.ToString());
        GameManager.instance.seedNum = number;
        PlayerPrefs.SetInt("seedNumber", number);

        // Map Type Settings
        if (mapOfDay.isOn == true)
        {
            GameManager.instance.mapMode = 0;
            PlayerPrefs.SetInt("mapMode", 0);
        }
        else if (randomMap.isOn == true)
        {
            GameManager.instance.mapMode = 1;
            PlayerPrefs.SetInt("mapMode", 1);
        }
        else if (seededMap.isOn == true)
        {
            GameManager.instance.mapMode = 2;
            PlayerPrefs.SetInt("mapMode", 2);
        }
    }
}
