using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetName : MonoBehaviour
{
    private string itemName;
    private GameObject itemNameText;
    [SerializeField] public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        itemNameText = this.gameObject.GetComponentInChildren<TextMeshProUGUI>().gameObject;
        itemName = prefab.name;

        itemNameText.GetComponent<TextMeshProUGUI>().text = itemName;
    }
}
