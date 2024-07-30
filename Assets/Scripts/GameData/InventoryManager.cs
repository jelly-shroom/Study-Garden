using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public GameObject _gameObj;
    //get currencyCount from Currency Helper script
    private GameObject currencyCount;
    public int plantCost;

    //need to link inventoy manager to currency helper and shop item
    //if currency count is greater than or equal to item cost, then the item is enabled
    //if the item is enabled, add it to the plant inventory list
    //need to access itemCost from shop item, but shop item is disabled when inventorymanager is enabled
    //itemCost is linked to plantBehavior as plantCost, attached to the plant prefab
    //need to access plantCost from plantBehavior

    public void Start()
    {

    }

    public void EnabledItems()
    {
        currencyCount = GameObject.FindGameObjectsWithTag("Currency")[0];
        Debug.Log("Currency Count: " + currencyCount.GetComponent<Currency>().counter);

        plantCost = this.gameObject.GetComponent<PlaceObj>().plantPrefab.GetComponent<PlantBehavior>().plantCost;
        Debug.Log("Plant Cost: " + plantCost);


        if (currencyCount.GetComponent<Currency>().counter >= plantCost && _gameObj.gameObject.activeSelf == false)
        {
            _gameObj.gameObject.SetActive(true);
            GameObject.Find("DataPersistenceManager").GetComponent<SaveableUIData>().plantInventoryList.Add(_gameObj.gameObject);

            currencyCount.GetComponent<Currency>().counter -= plantCost;
        }
    }

}
