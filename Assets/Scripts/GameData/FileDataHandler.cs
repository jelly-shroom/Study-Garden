using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            Debug.Log("Found file");
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                //deserialize the data to C# obj
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            } catch (Exception e) {
                Debug.LogError("error when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Debug.Log(Application.persistentDataPath);
        try 
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize gamedata obj into json str
            string dataToStore = JsonUtility.ToJson(data, true);

            //write file to system
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        } catch (Exception e) {
            Debug.LogError("error while trying to save to file: " + fullPath + "\n" + e);
        }
    }
}
