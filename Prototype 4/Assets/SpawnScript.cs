using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    private Vector3 spawnPoint;
    void Start()
    {
        
        Instantiate(enemyPrefab, SpawnGenerator(), enemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 SpawnGenerator()
    {
        float spawnPosX = Random.Range(-9, 9);
        float spawnPosZ = Random.Range(-9, 9);
        spawnPoint = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPoint;
    }
}
