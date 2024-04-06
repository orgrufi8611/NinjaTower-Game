using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    [SerializeField] float destroyDistanceFromBurrom;
    public GameLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < gameLogic.bottom.position.y - destroyDistanceFromBurrom)
        {
            Destroy(gameObject);
        }
    }
}
