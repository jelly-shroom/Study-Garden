using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public int currencyCount;

    public List<Plant> plants;
    public List<GameObject> plantInventoryList;

    public GameObject[] taskArray;
    public List<string> taskTextList;

    public GameData()
    {
        currencyCount = 0;

        plants = new List<Plant>();
        plantInventoryList = new List<GameObject>();

        taskArray = new GameObject[0];
        taskTextList = new List<string>();
    }
}