using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private string masterVol = "Master Volume";
    [SerializeField] private AudioMixer mixer;

    //Check that the min is 0.0001
    [SerializeField] private Slider slider;
    [SerializeField] private float sliderSesitivity = 30f;
    [SerializeField] Toggle toggle;
    private bool disableToggleEvent;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderChange);
        toggle.onValueChanged.AddListener(HandleToggleChange);
    }

    private void HandleToggleChange(bool enableSound)
    {
        //prevents a looped-y loop
        if (disableToggleEvent) return;

        if(enableSound)
        {
            slider.value = PlayerPrefs.GetFloat(masterVol);
        }
        else
        {
            PlayerPrefs.SetFloat(masterVol, slider.value);
            slider.value = slider.minValue;
        }
    }

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(masterVol);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(masterVol, slider.value);
    }

    private void HandleSliderChange(float value)
    {               
        mixer.SetFloat(masterVol, Mathf.Log10(value) * sliderSesitivity);
        disableToggleEvent = true;
        toggle.isOn = slider.value > slider.minValue;
        disableToggleEvent = false;
    }   
}
