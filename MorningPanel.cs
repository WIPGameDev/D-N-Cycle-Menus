using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorningPanel : MonoBehaviour
{
    [SerializeField] private DayNightCycle dayNightCycle;
    [SerializeField] Text panelText;
    [SerializeField] RawImage panelImage;
    [SerializeField] private float panelActiveTime;   
    private float panelActiveCounter;
        
    // Start is called before the first frame update
    void OnEnable()
    {
        dayNightCycle.OnDayEnd += activatePanel;
    }

    private void OnDisable()
    {
        dayNightCycle.OnDayEnd -= activatePanel;
    }

    private void activatePanel(int currentDay)
    {
        panelText.enabled = true;
        panelImage.enabled = true;
        panelActiveCounter = panelActiveTime;
        panelText.text = "Good morning, \n it is day " + currentDay + " !";
    }

    // Update is called once per frame
    void Update()
    {
        if(panelActiveCounter > 0)
        {
            panelActiveCounter -= Time.deltaTime;
        }
        
        if(panelActiveCounter <= 0)
        {
            panelText.enabled = false;
            panelImage.enabled = false;
        }
    }
}
