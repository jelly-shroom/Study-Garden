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

    private CurrencyHelper currencyCounter;

    private void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();

        timerSeconds = 0.0f;
        timerMinutes = 0;
        timerHours = 0;

        currencyCounter = FindAnyObjectByType<CurrencyHelper>();
    }

    private void Update()
    {
        if (timer) //while the timer is running
        {
            //timer function
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


            //reward with currency after set interval of time
            if (timerMinutes % rewardIntervalMinutes == 0 && timerSeconds == 0)
            {
                //Debug.Log("Reward!");
                currencyCounter.currencyCount[0].GetComponent<Currency>().counter += 3;
            }

            //milestone reward
            foreach (int milestone in milestones)
            {
                if (timerMinutes == milestone && timerSeconds == 0)
                {
                    currencyCounter.currencyCount[0].GetComponent<Currency>().counter += 5;
                }
            }
        }
    }
}