using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlantBehavior : MonoBehaviour
{
    GameObject dataManagerObj;
    GameObject gameManager;
    public bool editMode;
    public Vector3 position;
    private GameObject spawnPoint;
    private GameObject editGardenMenu;
    [SerializeField] GameObject confirmButton;
    [SerializeField] GameObject deleteButton;

    private Camera _mainCamera;
    private Renderer _renderer;

    private Ray _ray;
    private RaycastHit _hit;

    public int plantCost;

    private Vector3 initialMousePosition;
    private float dragThreshold = 10f; // Pixels threshold to consider it a drag vs. a click

    void Start()
    {
        _mainCamera = Camera.main;
        _renderer = GetComponent<Renderer>();

        dataManagerObj = GameObject.Find("DataPersistenceManager");
        editGardenMenu = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().editGardenMenu.gameObject;
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn");

        position = gameObject.transform.position;

        if (editGardenMenu.activeSelf)
        {
            editMode = true;
            confirmButton.SetActive(true);
            deleteButton.SetActive(true);
        }
        else
        {
            editMode = false;
            confirmButton.SetActive(false);
            deleteButton.SetActive(false);
        }
    }

    void Update()
    {
        if (editMode == true)
        {
            // This makes sure that the position gets saved only once
            var oneTime = false;

            Vector3 newPosition = position;

            // Moving obj and positioning
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                newPosition += Vector3.right;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                newPosition += Vector3.left;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                newPosition += Vector3.forward;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                newPosition -= Vector3.forward;
            }

            // Check if the new position is within the bounding box
            if (newPosition.x < 0 || newPosition.x > gameManager.GetComponent<GameManager>().boundingBox.x ||
                newPosition.z < 0 || newPosition.z > gameManager.GetComponent<GameManager>().boundingBox.z)
            {
                Debug.Log("New position is out of bounds: " + newPosition);
                return;
            }

            // Move the plant regardless of occupation
            position = newPosition;
            gameObject.transform.position = position;

            // If the edit menu is closed, save the position
            if (editGardenMenu.activeSelf == false && !oneTime)
            {
                ConfirmPosition();
                gameManager.GetComponent<GameManager>().isPlantBeingEdited = false;

                oneTime = true;
            }
        }

        // Only runs CheckClickPlant if a plant isn't currently being edited
        if (gameManager.GetComponent<GameManager>().isPlantBeingEdited == false)
        {
            HandleClickPlant();
        }
    }

    private void HandleClickPlant()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = Input.mousePosition; // Record the initial mouse position
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Check if the mouse moved beyond the drag threshold
            if (Vector3.Distance(initialMousePosition, Input.mousePosition) <= dragThreshold)
            {
                // If within the drag threshold, it's a valid click
                CheckClickPlant();
            }
        }
    }

    public void ConfirmPosition()
    {
        // Check if the current position is occupied by another plant
        bool positionOccupied = false;
        foreach (Transform child in spawnPoint.transform)
        {
            if (child.position == position && child != transform) // exclude self
            {
                positionOccupied = true;
                break;
            }
        }

        if (!positionOccupied)
        {
            editMode = false;
            confirmButton.SetActive(false);
            deleteButton.SetActive(false);
            GameObject.Find("DataPersistenceManager").GetComponent<SaveableWorldData>().plants.Add(new Plant(this.gameObject.name, this.gameObject.transform.position));

            gameManager.GetComponent<GameManager>().isPlantBeingEdited = false;
        }
        else
        {
            Debug.Log("Cannot confirm position, another plant is occupying this spot: " + position);
        }
    }

    public void CheckClickPlant()
    {
        // Clicking obj to edit position
        _ray = new Ray(_mainCamera.ScreenToWorldPoint(Input.mousePosition), _mainCamera.transform.forward);

        if (Physics.Raycast(_ray, out _hit, 1000f))
        {
            if (_hit.transform == transform && editGardenMenu.activeSelf)
            {
                Debug.Log("clicked " + gameObject.name);
                confirmButton.SetActive(true);
                deleteButton.SetActive(true);
                editMode = true;

                gameManager.GetComponent<GameManager>().isPlantBeingEdited = true;

                // Deletes the previously saved position so it won't save duplicates
                // Iterate through each of the locations of the plants and delete the one that matches
                foreach (Plant plant in dataManagerObj.GetComponent<SaveableWorldData>().plants.ToList())
                {
                    if (plant.position == gameObject.transform.position)
                    {
                        dataManagerObj.GetComponent<SaveableWorldData>().plants.Remove(plant);
                    }
                }
            }
        }
    }

    public void DestroyPlantObject()
    {
        Destroy(this.gameObject);
        gameManager.GetComponent<GameManager>().isPlantBeingEdited = false;
    }

    public void PlantGrowth()
    {
        // Need to access amount of time passed in the study timer since the plant was instantiated
        // If current seconds/minutes/hours - seconds/minutes/hours when instantiated = [public int], replace mesh with another
    }
}
