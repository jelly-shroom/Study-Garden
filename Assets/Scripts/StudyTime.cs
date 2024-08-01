using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudyTime : MonoBehaviour
{
    public float timerSeconds;
    private int timerMinutes;
    private int timerHours;
    private TextMeshProUGUI timer;
    public List<int> milestones = new List<int>();
    public int rewardIntervalMinutes = 3;

    private void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();

        timerSeconds = 0.0f;
        timerMinutes = 0;
        timerHours = 0;
    }

    private void Update()
    {
        if (timer) //while the timer is running
        {
            timerSeconds += Time.deltaTime;

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
    }

    //to set up reward system for studying
    //milestones are manually added
    //need: give bonus points based on whether the timerSpan is equal to milestone


}