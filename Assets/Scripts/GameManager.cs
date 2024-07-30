using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject editGardenMenu;
    void Start()
    {
        editGardenMenu = GameObject.FindGameObjectWithTag("EditMenu");
        editGardenMenu.SetActive(false);
    }
}
