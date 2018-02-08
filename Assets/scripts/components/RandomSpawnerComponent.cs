using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerComponent : MonoBehaviour {

	public float secondsBetweenSpawns;
	public float secondsBeforeStart; 
	public GameObject[] attackPrefabs;

	private Timer spawnTimer;
	private Timer startTimer;
	
	// Use this for initialization
	void Start () {
		startTimer = new Timer(secondsBeforeStart);
		startTimer.Start();
		
		spawnTimer = new Timer(secondsBetweenSpawns);
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer.Finished() && startTimer.Finished()) {
			GameObject toSpawn = attackPrefabs[Random.Range(0, attackPrefabs.Length)];
			Instantiate(toSpawn, Vector3.zero, Quaternion.identity);
			
			spawnTimer = new Timer(secondsBetweenSpawns);
			spawnTimer.Start();
		}
	}
}
