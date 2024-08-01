using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]

//for tasks: save the gameobject (?) and the string data written in the tasks. 
//alternatively, save the number of clones currently in the scene rn. instantiate the prefab that many times, and iterate through each one and rename the string.

//using the gameobjects doesnt work because it tries to look for the game object within the scene, which is already destroyed. this needs to be approached in the same way i approached the plant data

//save the number of tasks there are in the list. count it. save a list of gameobjects/names(?) and count the number of items in the list. 
//create variable that storesthe task prefab and set it to instantiate with # = list.length
//text data also needs to be stored and set to the text within the instantiated prefab

public class SaveableUIData : MonoBehaviour, IDataPersistence
{
    public GameObject[] taskArray;
    public List<string> taskTextList;
    public GameObject taskPrefab;
    public GameObject taskParent;

    public List<string> plantInventoryList;

    void Start()
    {

    }

    public void LoadData(GameData data)
    {
        //loads task list
        this.taskTextList = data.taskTextList;
        foreach (string taskText in taskTextList)
        {
            GameObject taskSingularity = Instantiate(taskPrefab, Vector3.zero, Quaternion.identity, taskParent.transform);
            //needs to get the text component of the object
            GameObject text = taskSingularity.transform.GetChild(1).gameObject;
            text.GetComponent<TMP_InputField>().text = taskText;

            Debug.Log("Instantiated task: " + text.GetComponent<TMP_InputField>().text);
        }

        //loads plant inventory
        this.plantInventoryList = data.plantInventoryList;
        foreach (string plantUIName in data.plantInventoryList)
        {
            Debug.Log("Plant UI Name: " + plantUIName);

            //plantUIName will be disabled by default
            //need to find the disabled gameobject and set it to active

            //loop through every gameobject with inventory manager script
            //if the gameobject name is equal to the plantUIName, set it to active
            foreach (InventoryManager inventory in GameObject.FindObjectsOfType<InventoryManager>(true))
            {
                if (inventory.gameObject.name == plantUIName)
                {
                    inventory.gameObject.SetActive(true);
                }
            }
        }
    }

    public void SaveData(GameData data)
    {
        this.taskTextList.Clear();
        this.taskArray = GameObject.FindGameObjectsWithTag("Task");
        foreach (GameObject task in taskArray)
        {
            this.taskTextList.Add(task.GetComponent<TMP_InputField>().text);
            Debug.Log(task.GetComponent<TMP_InputField>().text);
        }
        data.taskArray = this.taskArray;
        data.taskTextList = this.taskTextList;

        data.plantInventoryList = this.plantInventoryList;
    }
}