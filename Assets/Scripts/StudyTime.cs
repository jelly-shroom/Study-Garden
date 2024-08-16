using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudyTime : MonoBehaviour
{
    [SerializeField] float timerSeconds;
    private int timerMinutes;
    private int timerHours;
    private TextMeshProUGUI timer;
    [SerializeField] List<int> milestones = new List<int>();
    [SerializeField] int rewardIntervalMinutes = 3;

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
                PopUpManager.Instance.ShowPopup("You have earned 3 currency!");
                currencyCounter.currencyCount[0].GetComponent<Currency>().counter += 3;
            }

            //milestone reward
            foreach (int milestone in milestones)
            {
                if (timerMinutes == milestone && timerSeconds == 0)
                {
                    PopUpManager.Instance.ShowPopup("Congratulations on meeting a milestone! You have earned 5 currency.");
                    currencyCounter.currencyCount[0].GetComponent<Currency>().counter += 5;
                }
            }
        }
    }

    private void OnDisable()
    {
        // Reset the timer when this GameObject is disabled
        timerSeconds = 0.0f;
        timerMinutes = 0;
        timerHours = 0;
        if (timer != null)
        {
            timer.text = "00:00";
        }
    }
}