using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public GameObject _gameObj;

    public void EnabledItems()
    {
        _gameObj.gameObject.SetActive(true);
        GameObject.Find("DataPersistenceManager").GetComponent<SaveableUIData>().plantInventoryList.Add(_gameObj.gameObject);
    }

}
