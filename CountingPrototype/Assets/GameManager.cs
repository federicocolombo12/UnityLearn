using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject towerPrefab;

    

    public void SpawnTower()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Imposta la distanza dalla telecamera
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        Instantiate(towerPrefab, worldPos, Quaternion.identity);
    }
   
}
