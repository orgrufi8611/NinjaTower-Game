using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void StartGame()
    {
        GameObject.Find("SceneController").GetComponent<SceneController>().SceneLoad(SceneController.sceneNames[1]);
    }
}
