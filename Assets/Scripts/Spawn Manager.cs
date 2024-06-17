using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    private PlayerController playerControllerScript;
    private float randomRange = 9f;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnEnemyWave(waveNumber);
        enemyPrefabs[1].GetComponent<Enemy>().speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControllerScript.isGameOver == true)
        {
            StopAllCoroutines();
            return;
        }
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            //Spawn a boss every x number of waves
            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
            //Updated to select a random powerup prefab for the Medium Challenge
            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], SpawnRandomPosition(),
            powerupPrefabs[randomPowerup].transform.rotation);
        }
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {

        for(int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0,enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], SpawnRandomPosition(), enemyPrefabs[randomEnemy].transform.rotation);
        }
        enemyPrefabs[1].GetComponent<Enemy>().speed *= 1.1f;
    }
    private Vector3 SpawnRandomPosition()
    {
        float spawnPosX = Random.Range(-randomRange, randomRange);
        float spawnPosZ = Random.Range(-randomRange, randomRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0.16f, spawnPosZ);
        return randomPos;
    }
    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;
        //We dont want to divide by 0!
        if (bossRound != 0)
        {
            miniEnemysToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }
        var boss = Instantiate(bossPrefab, SpawnRandomPosition(),
        bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;
    }
    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], SpawnRandomPosition(),
            miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }

}
