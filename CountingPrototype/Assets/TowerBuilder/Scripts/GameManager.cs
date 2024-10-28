using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   public class GameManager : MonoBehaviour
{
    public GameObject towerPrefab;

    public void SpawnTower()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 10f); // Center of the screen and desired distance
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenCenter);

        Instantiate(towerPrefab, worldPos, Quaternion.identity);
    }
}

