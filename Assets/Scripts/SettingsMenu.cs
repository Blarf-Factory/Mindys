﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    //GENERNAL GLOBALS

    //AUDIO GLOBALS
    public AudioMixer MusicAudio;
    public AudioMixer SoundAudio;

    //GRAPHIC GLOBALS
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    //CONTROL GLOBALS

    //MISC GLOBALS
    public GameObject GeneralSettings;
    public GameObject GraphicSettings;
    public GameObject AudioSettings;
    public GameObject ControlSettings;

    // Start is called before the first frame update
    void Start()
    {
        getResolutions();
    }

/***************GENERAL**SETTINGS***************/
/***********************************************/

    public void DefaultGeneralSettings()
    {
        
    }

    /****************AUDIO**SETTINGS****************/
    /***********************************************/
    /*
    public void setMasterVolume(float v)
    {   
        float m;
        float a;
        MusicAudio.GetFloat("MusicVolume", out m);
        m = (m / 2) - 40;
        SoundAudio.GetFloat("SoundVolume", out a);
        a = (a / 2) - 40;
        MusicAudio.SetFloat("MusicVolume", m);
        SoundAudio.SetFloat("MusicVolume", a);
    }*/

    public void setMusicVolume(float v)
    {
        MusicAudio.SetFloat("MusicVolume", v);
    }

    public void setSoundVolume(float v)
    {
        SoundAudio.SetFloat("SoundVolume", v);
    }

    public void DefaultAudioSettings()
    {
        
    }

/****************CONTROL**CONFIG****************/
/***********************************************/

    public void LoadControls()
    {
        
    }

    public void SaveControls()
    {
        
    }

    public void DefaultControls()
    {
        
    }

    /***************GRAPHIC**SETTINGS***************/
    /***********************************************/

    public void DefaultGraphicSettings()
    {
        SetResolution();
        GraphicLevel(3);
        SetFullScreen(true);
    }

    void getResolutions(){
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);
            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolutions(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    public void SetFullScreen(bool fs)
    {
        Screen.fullScreen = fs;
    }

    public void GraphicLevel(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }

    public void SetResolution()
    {
        
    }

/****************MISC***SETTINGS****************/
/***********************************************/

    public void DefaultSettings()
    {
        DefaultGeneralSettings();
        DefaultAudioSettings();
        DefaultControls();
        DefaultGraphicSettings();
    }

    public void changeSettingType(GameObject UI)
    {
        GeneralSettings.SetActive(false);
        GraphicSettings.SetActive(false);
        AudioSettings.SetActive(false);
        ControlSettings.SetActive(false);
        
        UI.SetActive(true);
    }

    public void LoadSettings()
    {
        
    }

    public void SaveSettings()
    {
        
    }

    public void Back()
    {
        
    }
}
