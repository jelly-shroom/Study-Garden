using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlantBehavior : MonoBehaviour
{
    GameObject dataManagerObj;
    GameObject gameManager;
    public bool editMode;
    public Vector3 position;
    private GameObject editGardenMenu;
    public GameObject confirmButton;
    public GameObject deleteButton;

    private Camera _mainCamera;
    private Renderer _renderer;

    private Ray _ray;
    private RaycastHit _hit;

    public int plantCost;

    void Start()
    {
        _mainCamera = Camera.main;
        _renderer = GetComponent<Renderer>();

        dataManagerObj = GameObject.Find("DataPersistenceManager");
        editGardenMenu = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().editGardenMenu.gameObject;
        gameManager = GameObject.FindGameObjectWithTag("GameController");

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
            //this makes sure that the position gets saved only once
            var oneTime = false;

            //moving obj and positioning
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                position += Vector3.right;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                position += Vector3.left;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                position += Vector3.forward;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                position -= Vector3.forward;
            }

            gameObject.transform.position = position;



            //if the edit menu is closed, save the position
            if (editGardenMenu.activeSelf == false && !oneTime)
            {
                ConfirmPosition();
                gameManager.GetComponent<GameManager>().isPlantBeingEdited = false;

                oneTime = true;
            }
        }

        //only runs checkclickplant if a plant isnt currently being edited
        if (gameManager.GetComponent<GameManager>().isPlantBeingEdited == false)
        {
            CheckClickPlant();
        }
    }


    public void CheckClickPlant()
    {
        //clicking obj to edit position
        if (Input.GetMouseButtonDown(0))
        {
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

                    //deletes the previously saved position so it won't save duplicates
                    //iterate through each of the locations of the plants and delete the one that match
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
    }

    public void ConfirmPosition()
    {
        editMode = false;
        confirmButton.SetActive(false);
        deleteButton.SetActive(false);
        GameObject.Find("DataPersistenceManager").GetComponent<SaveableWorldData>().plants.Add(new Plant(this.gameObject.name, this.gameObject.transform.position));

        gameManager.GetComponent<GameManager>().isPlantBeingEdited = false;
    }

    public void DestroyPlantObject()
    {
        Destroy(gameObject);

        gameManager.GetComponent<GameManager>().isPlantBeingEdited = false;
    }

    public void PlantGrowth()
    {
        //need to access amount of time passed in the study timer since the plant was instantiated
        //if current seconds/minutes/hours - seconds/minutes/hours when instantiated = [public int], replace mesh with another
    }
}
