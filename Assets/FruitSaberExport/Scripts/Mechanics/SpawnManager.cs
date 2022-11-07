using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public GameObject[] fruitPrefab;
    public GameObject bombPrefab;
	public Transform[] spawnPoints;

    public void SpawnOneFruit()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        GameObject spawnedFruit = Instantiate(fruitPrefab[Random.Range(0,fruitPrefab.Length)], spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnOneBomb()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];
        GameObject spawnedBomb = Instantiate(bombPrefab, spawnPoint.position, spawnPoint.rotation);
    }
	
}

