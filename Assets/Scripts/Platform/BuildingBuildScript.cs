using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuildScript : MonoBehaviour
{
    [Header("Building Prefub")]
    [SerializeField] GameObject buildingPart;
    
    [Header("Building Wallpapers")]
    [SerializeField] Material[] materials;

    [Header("Building Parameters")]
    [SerializeField] Transform player;
    [SerializeField] float distanceYOffset;
    [SerializeField] float distanceToSpawn;
    [SerializeField] Vector3 spawnPos;
    int counter;

    [Header("Game Objects")]
    [SerializeField] CoinSpawner coinSpawner;

    [Header("Buildings Parent")]
    [SerializeField] GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        spawnPos = new Vector3(0,0,0);
        for (int i = 0; i < 10; i++)
        {
            SpawnBuildingPart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(player.position.y-spawnPos.y) < distanceToSpawn)
        {
            SpawnBuildingPart();
        }
    }

    void SpawnBuildingPart()
    {
        GameObject part = Instantiate(buildingPart,spawnPos,Quaternion.Euler(0,180,0));
        part.transform.SetParent(parent.transform);
        part.GetComponent<BuildingPainter>().wallpaper = materials[counter % materials.Length];
        float r = Random.Range(0f, 1f);
        if(r < 0.5f)
        {
            coinSpawner.SpawnVertical(spawnPos);
        }
        spawnPos.y += distanceYOffset;
        counter++;
    }
}

