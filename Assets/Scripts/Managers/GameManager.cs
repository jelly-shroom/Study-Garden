using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject editGardenMenu;
    public bool isPlantBeingEdited = false;
    public Vector3 boundingBox;

    void Start()
    {
        editGardenMenu = GameObject.FindGameObjectWithTag("EditMenu");
        editGardenMenu.SetActive(false);

        boundingBox = new Vector3(10, 1, 10);
    }
}
