using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObj : MonoBehaviour
{
    GameObject gameManager;
    Vector3 boundingBox;
    private GameObject spawnPoint;
    public GameObject plantPrefab;
    public Vector3 spawnLocation;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
        boundingBox = gameManager.GetComponent<GameManager>().boundingBox;
    }

    public void AddObject()
    {
        //check if there are plants active
        if (spawnPoint.transform.childCount != 0)
        {
            //loop through all children of spawn point and if editMode is true, confirm position of child
            foreach (Transform child in spawnPoint.transform)
            {
                if (child.GetComponent<PlantBehavior>().editMode == true)
                {
                    child.GetComponent<PlantBehavior>().ConfirmPosition();
                }
            }
        }
        gameManager.GetComponent<GameManager>().isPlantBeingEdited = true;


        //spwan object in blank location
        //check if location is empty. if empty, spawn. if not, increase x or z by 1 and check again
        //x and z locations should never exceed bounding box
        spawnLocation = new Vector3(0, 1, 0);
        foreach (Transform child in spawnPoint.transform)
        {
            if (child.position == spawnLocation)
            {
                spawnLocation.x += 1;
                if (spawnLocation.x > boundingBox.x)
                {
                    spawnLocation.x = 0;
                    spawnLocation.z += 1;
                    if (spawnLocation.z > boundingBox.z)
                    {
                        Debug.Log("No more space to plant");
                        gameManager.GetComponent<GameManager>().isPlantBeingEdited = false;

                        return;
                    }
                }
            }
        }


        Instantiate(plantPrefab, spawnLocation, Quaternion.identity, spawnPoint.transform);

    }
}
