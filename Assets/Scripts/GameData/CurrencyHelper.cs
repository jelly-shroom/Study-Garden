using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyHelper : MonoBehaviour, IDataPersistence
{
    //task completion
    public int currencyGain = 2;
    public GameObject[] currencyCount;

    //purchases
    public int itemCost;
    [SerializeField] public TextMeshProUGUI itemCost_text;

    void Start()
    {
        currencyCount = GameObject.FindGameObjectsWithTag("Currency");

        string plantName = this.gameObject.name.Replace("_shop", string.Empty);
        Debug.Log("Plant Name: " + plantName);

        // itemCost = GameObject.Find(plantName).GetComponent<PlantBehavior>().plantCost;
        itemCost = GameObject.Find("DataPersistenceManager").GetComponent<SaveableWorldData>().plantPrefabList.Find(obj => obj.name == plantName).GetComponent<PlantBehavior>().plantCost;
        itemCost_text.text = "" + itemCost;
    }

    void Update()
    {
        //all objects in currencyCount array should have the same counter
        foreach (GameObject currency in currencyCount)
        {
            currency.GetComponent<Currency>().counter = currencyCount[0].GetComponent<Currency>().counter;
        }

        //count cannot go below 0
        if (currencyCount[0].GetComponent<Currency>().counter < 0)
        {
            currencyCount[0].GetComponent<Currency>().counter = 0;
        }
    }



    public void LoadData(GameData data)
    {
        foreach (GameObject currency in currencyCount)
        {
            currency.GetComponent<Currency>().counter = data.currencyCount;
        }
    }

    public void SaveData(GameData data)
    {
        data.currencyCount = currencyCount[0].GetComponent<Currency>().counter;
    }

    public void CountPoints()
    {
        currencyCount[0].GetComponent<Currency>().counter += currencyGain;

        Debug.Log("Currency: " + currencyCount[0].GetComponent<Currency>().counter);
    }

}
