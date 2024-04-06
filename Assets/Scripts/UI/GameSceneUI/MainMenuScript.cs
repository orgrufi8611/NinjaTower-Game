using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] Slider bgm;
    [SerializeField] Slider sfx;
    [SerializeField] Toggle bgmCheckBox;
    [SerializeField] Toggle sfxCheckBox;
    float sfxVolume;
    float bgmVolume;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("current BGM volume: " + VolumeSettings.BGMVolume + " SFX volume " + VolumeSettings.SFXVolume);
        sfx.value = VolumeSettings.SFXVolume;
        bgm.value = VolumeSettings.BGMVolume;
        //GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeBGM(bgm.value);
        //GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeSFX(sfx.value);
        settingsMenu.SetActive(false);
    }

    public void StartButton()
    {
        GameObject.Find("SceneController").GetComponent<SceneController>().SceneLoad(SceneController.sceneNames[1]);
    }

    public void SettingButton()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void SetBGMVolume()
    {
        float value = bgm.value;
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeBGM(value);
    }

    public void SetSFXVolume()
    {
        float value = sfx.value;
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeSFX(value);
    }

    public void BGMCheckBox()
    {
        if (bgmCheckBox.isOn)
        {
            UnmuteBGM();
        }
        else
        {
            MuteBGM();
        }
    }

    public void SFXCheckBox()
    {
        if (sfxCheckBox.isOn)
        {
            UnmuteSFX();
        }
        else
        {
            MuteSFX();
        }
    }

    void MuteBGM()
    {
        bgmVolume = bgm.value;
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeBGM(bgm.minValue);
        bgm.value = bgm.minValue;
    }

    void MuteSFX()
    {
        sfxVolume = sfx.value;
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeSFX(sfx.minValue);
        sfx.value = sfx.minValue;
    }

    void UnmuteBGM()
    {
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeBGM(bgmVolume);
        bgm.value = bgmVolume;
    }

    void UnmuteSFX()
    {
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeSFX(sfxVolume);
        sfx.value = sfxVolume;
    }
}
