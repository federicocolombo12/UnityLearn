using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Items;
    private int spawnTimer=2;
    void Start()
    {
        StartCoroutine(SpawnItems());
    }

    // Update is called once per frame
    IEnumerator SpawnItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            int index = Random.Range(0, Items.Count);
            Instantiate(Items[index]);
        }
        

    }
}
