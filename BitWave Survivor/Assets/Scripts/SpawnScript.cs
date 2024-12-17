using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    private int enemyNumber;
    public int wave = 1;
    public GameObject powerUpPrefab;
    private Transform playerPosition;
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        enemyNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyNumber == 0)
        {
            
            SpawnPrefab(wave);
            wave++;
        }
    }
    void SpawnPrefab(int wave)
    {
        for (int i = 0; i < wave; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
            
        }
        Instantiate(powerUpPrefab, GenerateSpawnPos2(), powerUpPrefab.transform.rotation);
    }
    Vector2 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-24, 24);
        float spawnPosY = Random.Range(0, 10);
        return new Vector2(spawnPosX, spawnPosY);
    }
    Vector2 GenerateSpawnPos2()
    {
        float spawnPosX = Random.Range(-24, 24);
        
        return new Vector2(spawnPosX, 0);
    }
}
