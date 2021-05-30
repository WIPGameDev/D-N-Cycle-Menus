using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNightCycle : MonoBehaviour, IPersist
{
    [SerializeField] private Light sun;
    [SerializeField] private float secondsInFullDay = 120f;
    [Range(0, 1)] [SerializeField] private float currentTimeOfDay = 0.20f;
    private int currentDay = 1;

    [SerializeField] float startOfDay = 0.2f;
    [SerializeField] float endOfDay = 0.8f;

    [SerializeField] TMP_Text timeDisplay;

    private float timeMultiplier = 1f;
    private float sunInitialIntensity;

    public event Action<int> OnDayEnd;
    //The Fader and maybe a reset position script should listen to this event as well
    //This could be when the bank increases your debt, could be used by the refinery too

    void Start()
    {
        sunInitialIntensity = sun.intensity;
    }

    void Update()
    {
        UpdateSun();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        UpdateClock();

        if(currentTimeOfDay >= endOfDay)
        {
            EndOfDay();
        }
    }

    private void UpdateClock()
    {
        float timeInHours = Mathf.FloorToInt(currentTimeOfDay * 24f) + 1;
        string meridiem;
        if(timeInHours > 12)
        {
            timeInHours -= 12;
            meridiem = "PM";
        }
        else
        {
            meridiem = "AM";
        }

        timeDisplay.text = timeInHours.ToString() + " " + meridiem;
    }

    private void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        //Change Brightness across the day
        float intensityMultiplier = 1f;

        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0f;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1/0.02f));
        }
        else if(currentTimeOfDay <= .73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

    private void EndOfDay()
    {
        currentDay++;
        OnDayEnd?.Invoke(currentDay);      
        currentTimeOfDay = startOfDay;
    }

    public void Save(GameData gameData)
    {
        gameData.currentDay = currentDay;
        gameData.timeOfDay = currentTimeOfDay;
    }

    public void Load(GameData gameData)
    {
        currentDay = gameData.currentDay;
        currentTimeOfDay = gameData.timeOfDay;
    }
}
