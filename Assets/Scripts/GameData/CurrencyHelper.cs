using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyHelper : MonoBehaviour, IDataPersistence
{
    //task completion
    public int currencyGain = 2;
    public GameObject currencyCount;
    private GameObject currencyCountShop;

    //purchases
    public int itemCost;
    [SerializeField] public TextMeshProUGUI itemCost_text;

    void Start()
    {
        currencyCount = GameObject.Find("currencyAmount_text");
        currencyCountShop = GameObject.Find("currencyAmount_text (shop)");

        itemCost_text.text = "" + itemCost;
    }



    public void LoadData(GameData data)
    {
        currencyCount.GetComponent<Currency>().counter = data.currencyCount;
    }

    public void SaveData(GameData data)
    {
        data.currencyCount = currencyCount.GetComponent<Currency>().counter;
    }

    public void CountPoints()
    {
        currencyCount.GetComponent<Currency>().counter += currencyGain;
        currencyCountShop.GetComponent<Currency>().counter += currencyGain;

        Debug.Log("Currency: " + currencyCount.GetComponent<Currency>().counter);
    }

    public void Purchase()
    {
        currencyCount.GetComponent<Currency>().counter -= itemCost;
        currencyCountShop.GetComponent<Currency>().counter -= itemCost;
    }
}
