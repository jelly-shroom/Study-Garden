using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Plant
{
    public Vector3 position;
    public string name;
    // [SerializeField] public int cost;
    public Plant(string name, Vector3 position)
    {
        this.name = name;
        this.position = position;
    }

}


public class SaveableWorldData : MonoBehaviour, IDataPersistence
{
    private GameObject ground;
    public List<GameObject> plantPrefabList;
    public List<Plant> plants;

    public GameObject getPrefabForName(string name)
    {
        foreach (GameObject prefab in plantPrefabList)
        {
            string newName = name.Replace("(Clone)", string.Empty);
            Debug.Log(newName);

            if (prefab.name == newName)
            {
                return prefab;
            }
        }
        throw new System.Exception("prefab not found!!");
    }

    public void LoadData(GameData data)
    {
        ground = GameObject.FindGameObjectWithTag("Spawn");

        foreach (Plant plant in data.plants)
        {
            GameObject prefab = getPrefabForName(plant.name);
            GameObject loadedPlant = Instantiate(prefab, plant.position, Quaternion.identity, ground.transform);

            //save the data of the preexisting plants
            this.plants.Add(new Plant(loadedPlant.gameObject.name, loadedPlant.gameObject.transform.position));
        }
    }

    public void SaveData(GameData data)
    {
        data.plants.Clear();
        foreach (Plant plant in plants)
        {
            data.plants.Add(new Plant(plant.name, plant.position));
        }
        // data.plants.Clear();
    }
}
