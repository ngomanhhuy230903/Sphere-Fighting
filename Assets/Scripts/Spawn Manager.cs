using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private PlayerController playerControllerScript;
    private float randomRange = 9f;
    public int enemyCount;
    public int waveNumber = 1;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0 && playerControllerScript.isGameOver == false)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerUpPrefab, SpawnRandomPosition(), powerUpPrefab.transform.rotation);
        }
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, SpawnRandomPosition(), enemyPrefab.transform.rotation);
        }
    }
    private Vector3 SpawnRandomPosition()
    {
        float spawnPosX = Random.Range(-randomRange, randomRange);
        float spawnPosZ = Random.Range(-randomRange, randomRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0.16f, spawnPosZ);
        return randomPos;
    }
}
