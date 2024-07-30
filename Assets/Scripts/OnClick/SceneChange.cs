using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);

        if (sceneID == 0)
        {
            
        }
    }
    
    public void SaveData()
    {
        
    }
}
