using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    [SerializeField] Collider platformCollider;
    [SerializeField] Vector3 platformSize;
    [SerializeField] LayerMask player;
    [SerializeField] GameObject platform;
    
    public GameLogic gameLogic;
    public CoinSpawner coinSspawner;
    public bool movable;
    public bool floor;
    public float size = 0;
    public int platformNumber;
    public GameStatistics gameStatistics;
    
    public bool cleared;
    public bool skipped;

    [Header("Gizmo on/off")]
    [SerializeField] bool gizmoOn;
    
    bool init;
    Vector3 InitPos;
    float direction;
    
    // Start is called before the first frame update
    void Start()
    {
        gizmoOn = false;
        init = false;
        movable = false;
        cleared = false;
        skipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameLogic.active)
        {
            if (transform.position.y < gameLogic.bottom.position.y - 0.1f)
            {
                if (!cleared)
                {
                    Cleared();
                }
                Destroy(gameObject);
            }
            //not inplemented in the game yet
            if (movable)
            {
                
                Vector3.Lerp(transform.position, InitPos + direction * Vector3.right * size / 2, 3* Time.deltaTime);
                if(Vector3.Distance(transform.position, InitPos + direction * Vector3.right * size / 2) < 1)
                {
                    direction *= -1;
                }
            }
        }
    }

    public void Cleared()
    {
        if (!cleared)
        {
            gameLogic.addPlatformToScore(platformNumber);
            if(gameStatistics.platformsCleared < platformNumber)
                gameStatistics.platformsCleared = platformNumber;
            cleared = true;

            if (floor)
            {
                gameLogic.floorUp();
            }
        }
    }

    private void LateUpdate()
    {
        if (!init)
        {
            Physics.IgnoreCollision(platformCollider, gameLogic.player.gameObject.GetComponent<Collider>(),true);
            init = true;
            if (!floor)
            {
                float r = Random.Range(0f, 1f);
                if (r < 0.1f)
                {
                    coinSspawner.SpawnHorizantal(transform.position, size);
                }
            }
            platformSize.x *= size;
            CollisionCheck();
        }
    }

    void CollisionCheck()
    {
        if (!Physics.CheckBox(transform.position, platformSize, transform.rotation, player))
        {
            Physics.IgnoreCollision(platformCollider, gameLogic.player.gameObject.GetComponent<Collider>());
            if (gameLogic.changeColor)
                platform.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnDrawGizmos()
    {
        if (gizmoOn)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position + Vector3.up, platformSize);
        }
    }
}
