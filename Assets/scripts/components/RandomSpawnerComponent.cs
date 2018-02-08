using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerComponent : MonoBehaviour {

	public float secondsBetweenSpawns;
	public GameObject[] attackPrefabs;

	private Timer spawnTimer;
	
	// Use this for initialization
	void Start () {
		spawnTimer = new Timer(secondsBetweenSpawns);
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer.Finished()) {
			GameObject toSpawn = attackPrefabs[Random.Range(0, attackPrefabs.Length)];
			Instantiate(toSpawn, Vector3.zero, Quaternion.identity);
			
			spawnTimer = new Timer(secondsBetweenSpawns);
			spawnTimer.Start();
		}
	}
}
