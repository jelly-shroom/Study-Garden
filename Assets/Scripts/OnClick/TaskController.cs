using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.Mathematics;

public class TaskController : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    private GameObject taskList;

    void Start()
    {
        taskList = this.gameObject;
    }
    public void AddObject()
    {
        if (taskList.transform.childCount < 9)
        {
            Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        }
        else
        {
            PopUpManager.Instance.ShowPopup("You have reached the maximum number of tasks.");
        }
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
