using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBetweenScenes : MonoBehaviour
{
    
    void Start()
    { 
        for (int i = 0; i < Object.FindObjectsOfType<SaveBetweenScenes>().Length; i++)
        {
            if (Object.FindObjectsOfType<SaveBetweenScenes>()[i] != this)
            {
                if (Object.FindObjectsOfType<SaveBetweenScenes>()[i].name == name)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
