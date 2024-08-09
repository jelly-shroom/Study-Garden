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
        // Check if there are plants active
        if (spawnPoint.transform.childCount != 0)
        {
            // Loop through all children of spawn point and if editMode is true, confirm position of child
            foreach (Transform child in spawnPoint.transform)
            {
                if (child.GetComponent<PlantBehavior>().editMode == true)
                {
                    child.GetComponent<PlantBehavior>().ConfirmPosition();
                }
            }
        }
        gameManager.GetComponent<GameManager>().isPlantBeingEdited = true;

        // Start with an initial spawn location
        spawnLocation = new Vector3(0, 1, 0);

        bool locationFound = false;

        // Iterate over possible spawn locations within the bounding box
        for (float z = 0; z <= boundingBox.z; z += 1.0f)
        {
            for (float x = 0; x <= boundingBox.x; x += 1.0f)
            {
                Vector3 potentialLocation = new Vector3(x, 1, z);
                bool locationOccupied = false;

                // Check if any object is at the potential location
                foreach (Transform child in spawnPoint.transform)
                {
                    if (child.position == potentialLocation)
                    {
                        locationOccupied = true;
                        break;
                    }
                }

                // If the location is not occupied, use it for spawning
                if (!locationOccupied)
                {
                    spawnLocation = potentialLocation;
                    locationFound = true;
                    break;
                }
            }

            if (locationFound)
            {
                break;
            }
        }

        // If no location was found, log a message and stop the process
        if (!locationFound)
        {
            Debug.Log("No more space to plant");
            gameManager.GetComponent<GameManager>().isPlantBeingEdited = false;
            return;
        }

        // Instantiate the new plant at the found location
        Instantiate(plantPrefab, spawnLocation, Quaternion.identity, spawnPoint.transform);
    }

}
