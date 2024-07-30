using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObj : MonoBehaviour
{
    private GameObject spawnPoint;
    public GameObject plantPrefab;
    public Vector3 spawnLocation = new Vector3(1,1,1);

    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
    }

    public void AddObject()
    {
        Instantiate(plantPrefab, spawnLocation, Quaternion.identity, spawnPoint.transform);
    }
}
