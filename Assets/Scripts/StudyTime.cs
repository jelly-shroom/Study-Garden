using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 
public class StudyTime : MonoBehaviour
{
    public float timerSeconds = 0.0f;
    private int timerMinutes = 0;
    private int timerHours = 0;
    private TextMeshProUGUI timer;
    public List<int> milestones = new List<int>();
    public int rewardIntervalMinutes = 3;
 
    private void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
    }
 
    private void Update()
    {
        timerSeconds += Time.deltaTime;

        // TimeSpan timeSpan = TimeSpan.FromSeconds(timerSeconds);
        // timer.text = timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00");

        // if (timeSpan.Minutes % rewardIntervalMinutes == 0 && !rewardGranted)
        // {
        //     Debug.Log("Reward!");
        //     rewardGranted = true;
        // } else {
        //     rewardGranted = false;
        // }

        if (timerSeconds >= 60)
        {
            timerSeconds = 0.0f;
            timerMinutes++;
        }

        if (timerMinutes >= 60)
        {
            timerMinutes = 0;
            timerHours++;
        }

        timer.text = timerHours.ToString("00") + ":" + timerMinutes.ToString("00");

        if (timerMinutes % rewardIntervalMinutes == 0 && timerSeconds == 0)
        {
            Debug.Log("Reward!");
            //need access to data that stores the currency count and add to the data.game file
        }
    }

    //to set up reward system for studying
    //milestones are manually added
    //need: give bonus points based on whether the timerSpan is equal to milestone


}