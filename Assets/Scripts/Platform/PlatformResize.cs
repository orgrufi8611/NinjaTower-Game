using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformResize : MonoBehaviour
{
    public GameLogic gameLogic;
    public float size;
    [SerializeField] MeshRenderer floor;
    [SerializeField] MeshRenderer legs;
    [SerializeField] MeshRenderer arc;
    [SerializeField] PlatformMovement movementScript;


    // Start is called before the first frame update
    void Start()
    {
        size = Random.Range(gameLogic.platformMinSize, gameLogic.platformMaxSize);
        movementScript = GetComponent<PlatformMovement>();
        movementScript.size = size;
    }

    // Update is called once per frame
    void Update()
    {
        floor.transform.localScale = new Vector3(size, 1, 1);
        arc.transform.localScale = new Vector3(100/size, 100, 100);
    }
}
