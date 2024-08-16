using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public GameObject _gameObj;
    //get currencyCount from Currency Helper script
    private GameObject currencyCount;
    private int plantCost;


    public void PurchaseInventory()
    {
        currencyCount = GameObject.FindGameObjectsWithTag("Currency")[0];
        plantCost = this.gameObject.GetComponent<PlaceObj>().plantPrefab.GetComponent<PlantBehavior>().plantCost;

        if (currencyCount.GetComponent<Currency>().counter < plantCost)
        {
            PopUpManager.Instance.ShowPopup("You do not have enough currency to purchase this item.");
        }
        else if (_gameObj.gameObject.activeSelf == true)
        {
            PopUpManager.Instance.ShowPopup("You already have this item in your inventory.");
        }
        else
        {
            _gameObj.gameObject.SetActive(true);
            GameObject.Find("DataPersistenceManager").GetComponent<SaveableUIData>().plantInventoryList.Add(this._gameObj.gameObject.name);

            currencyCount.GetComponent<Currency>().counter -= plantCost;
        }
    }

}
