using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiScript : MonoBehaviour
{
   public GameLogic gameLogic;
    [SerializeField] Transform collisionCheck;
    [SerializeField] LayerMask player;
    [SerializeField] float radius;

    [Header("Gizmo on/off")]
    [SerializeField] bool gizmoOn;
    // Start is called before the first frame update
    void Start()
    {
        gizmoOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(collisionCheck.position, radius, player))
        {
            gameLogic.player.gameObject.GetComponent<PlayerController>().KunaiJump();
            //animation for Kunai use
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        if (gizmoOn)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(collisionCheck.position, radius);
        }
    }
}
