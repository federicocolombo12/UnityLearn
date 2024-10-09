using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUp;
    private Vector3 spawnPoint;
    public int enemyNumber;
    public int spawnNumber=1;
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        enemyNumber=FindObjectsOfType<Enemy>().Length;
        if (enemyNumber == 0)
        {
            SpawnEnemyWave(spawnNumber);
            Instantiate(powerUp, SpawnGenerator(), powerUp.transform.rotation);
            spawnNumber++;
        }
    }
    void SpawnEnemyWave(int enemyNumber)
    {
        for (int i = 0; i < enemyNumber; i++)
        {
            Instantiate(enemyPrefab, SpawnGenerator(), enemyPrefab.transform.rotation);
        }
    }
    private Vector3 SpawnGenerator()
    {
        float spawnPosX = Random.Range(-9, 9);
        float spawnPosZ = Random.Range(-9, 9);
        spawnPoint = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPoint;
    }
}
