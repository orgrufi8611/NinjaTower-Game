using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PauseUISCript : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] Slider sfx;
    [SerializeField] Slider bgm;
    [SerializeField] Toggle bgmCheckBox;
    [SerializeField] Toggle sfxCheckBox;
    [SerializeField] float bgmVolume;
    [SerializeField] float sfxVolume;

    private void Start()
    {
        Debug.Log("current BGM volume: " + VolumeSettings.BGMVolume + " SFX volume " + VolumeSettings.SFXVolume);
        sfx.value = VolumeSettings.SFXVolume;
        bgm.value = VolumeSettings.BGMVolume;
    }

    public void ExitButton()
    {
        Time.timeScale = 1;
        UI.GetComponent<UIScript>().Exit();
        GameObject.Find("SceneController").GetComponent<SceneController>().ImidiateLoad(SceneController.sceneNames[0]);
    }

    public void ResumeButton() 
    {  
        UI.GetComponent<UIScript>().Resume();
    }

    public void SetBGMVolume()
    {
        float value = bgm.value;
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeBGM(value);
        //UnmuteBGM();
    }

    public void SetSFXVolume()
    {
        float value = sfx.value;
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeSFX(value);
        //UnmuteSFX();
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
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeBGM(bgm.value);
        bgm.value = bgmVolume;
    }

    void UnmuteSFX()
    {
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeSFX(sfx.value);
        sfx.value = sfxVolume;
    }
}
