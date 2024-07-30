using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.Mathematics;

public class TaskController : MonoBehaviour
{
    public GameObject prefab;

    public void AddObject()
    {
        Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
