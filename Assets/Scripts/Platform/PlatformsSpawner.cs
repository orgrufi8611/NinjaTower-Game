using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlatformsSpawner : MonoBehaviour
{
    [Header("all objects prefubs")]
    [SerializeField] GameObject platformPrefub;
    [SerializeField] GameObject floorPrefub;
    [SerializeField] GameObject springPrefub;
    [SerializeField] GameObject kunaiPrefub;

    [Header("different Object Spawning chances")]
    [SerializeField] float springSpawnChance;
    [SerializeField] float kunaiSpawnChance;
    [SerializeField] float coinsSpawnChance;

    [Header("Different reffrences to Objects in scene")]
    [SerializeField] GameLogic gameLogic;
    [SerializeField] CoinSpawner coinSpawner;
    [SerializeField] GameStatistics gameStatistics;

    [Header("Platfroms parent")]
    [SerializeField] GameObject parent;
    public float lastY;
    int platformcount;

    // Start is called before the first frame update
    void Start()
    {
        platformcount = 0;
        lastY = 0;
        SpawnFloor();
        Spawn100Platforms();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameLogic.active)
        {
            if (Mathf.Abs(gameLogic.player.position.y - lastY) < 50)
            {
                Spawn100Platforms();
            }
        }
    }

    //spawn 100 platforms with chance for kunai and springs
    void Spawn100Platforms()
    {
        for (int i = 0; i < 99; i++)
        {
            float r = Random.Range(0f, 1f);

            if (i % 20 == 0 && i >= 20 && r < springSpawnChance)
            {
                SpawnSpring();
            }
            r = Random.Range(0f, 1f);
            if (i % 20 == 0 && i >= 20 && r < kunaiSpawnChance)
            {
                SpawnKunai();
            }
            
            SpawnPlatform();
        }
        //Spawn a floor after 100 platforms
        SpawnFloor();
    }

    void SpawnFloor()
    {
        GameObject floor = Instantiate(floorPrefub, new Vector3(0, lastY, 0), Quaternion.identity);
        floor.transform.SetParent(parent.transform);
        floor.GetComponent<PlatformMovement>().gameLogic = gameLogic;
        floor.GetComponent<PlatformMovement>().gameStatistics = gameStatistics;
        floor.GetComponent<PlatformMovement>().coinSspawner = coinSpawner;
        floor.GetComponent<PlatformMovement>().floor = true;
        floor.GetComponent<PlatformMovement>().platformNumber = platformcount;
        
        lastY += Random.Range(gameLogic.spawnIntervalmin, gameLogic.spawnIntervalmax);
        platformcount++;
    }
    void SpawnKunai()
    {
        float x = Random.Range(-(gameLogic.edges - gameLogic.platformMaxSize), gameLogic.edges - gameLogic.platformMaxSize);
        Vector3 pos = new Vector3(x, lastY, 0);
        GameObject kunai = Instantiate(kunaiPrefub, pos, Quaternion.identity);
        kunai.transform.SetParent(parent.transform);
        kunai.GetComponent<KunaiScript>().gameLogic = gameLogic;
        
        lastY += Random.Range(gameLogic.spawnIntervalmin, gameLogic.spawnIntervalmax);
    }

    void SpawnSpring()
    {
        float x = Random.Range(-(gameLogic.edges - gameLogic.platformMaxSize), gameLogic.edges - gameLogic.platformMaxSize);
        Vector3 pos = new Vector3(x, lastY, 0);
        GameObject spring = Instantiate(springPrefub,pos,Quaternion.identity);
        spring.transform.SetParent(parent.transform);
        spring.GetComponent<SpringScript>().gameLogic = gameLogic;
        
        lastY += Random.Range(gameLogic.spawnIntervalmin, gameLogic.spawnIntervalmax);
    }

    void SpawnPlatform()
    {
        float x = Random.Range(-(gameLogic.edges - gameLogic.platformMaxSize), gameLogic.edges - gameLogic.platformMaxSize);
        Vector3 pos = new Vector3(x, lastY, 0);
        GameObject platform = Instantiate(platformPrefub,pos,Quaternion.identity);

        platform.transform.SetParent(parent.transform);
        platform.GetComponent<PlatformMovement>().gameLogic = gameLogic;
        platform.GetComponent<PlatformMovement>().platformNumber = platformcount;
        platform.GetComponent<PlatformMovement>().coinSspawner = coinSpawner;
        platform.GetComponent<PlatformMovement>().gameStatistics = gameStatistics;
        platform.GetComponent<PlatformMovement>().floor = false;
        
        platform.GetComponent<PlatformResize>().gameLogic = gameLogic;
        
        lastY += Random.Range(gameLogic.spawnIntervalmin, gameLogic.spawnIntervalmax);
        platformcount++;
    }
}
