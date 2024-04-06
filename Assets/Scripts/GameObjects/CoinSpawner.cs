using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Coin prefub")]
    [SerializeField] GameObject coinPrefub;
    [SerializeField] float coinSize;
    
    [Header("Spawning parameters")]
    [SerializeField] int maxCoinSpawn;
    [SerializeField] GameLogic gameLogic;
    [SerializeField] Transform parent;
   

    public void SpawnHorizantal(Vector3 spawnPosition,float spawnSize)
    {
        maxCoinSpawn = (int)(spawnSize);
        Vector3 startPos = spawnPosition + new Vector3( - maxCoinSpawn * coinSize/2,coinSize/2,0);
        for (int i = 0; i < maxCoinSpawn; i++)
        {
            GameObject coin = Instantiate(coinPrefub,startPos,Quaternion.identity);
            coin.transform.SetParent(parent);
            coin.GetComponent<CoinScript>().gameLogic = gameLogic;
            startPos += Vector3.right * coinSize;
        }
    }

    public void SpawnVertical(Vector3 spawnPosition)
    {
        maxCoinSpawn = 10;
        float r = Random.Range(0f, 1f);
        Vector3 startPos = r > 0.5f ? gameLogic.rightWall.position - Vector3.right * coinSize/1.5f : gameLogic.leftWall.position + Vector3.right * coinSize / 1.5f;
        startPos.y = spawnPosition.y;
        for (int i = 0; i < maxCoinSpawn; i++)
        {
            GameObject coin = Instantiate(coinPrefub, startPos, Quaternion.identity);
            coin.transform.SetParent(parent);
            coin.GetComponent<CoinScript>().gameLogic = gameLogic;
            startPos += Vector3.up * coinSize;
        }
    }
    
}
