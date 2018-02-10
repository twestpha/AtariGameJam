using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerComponent : MonoBehaviour {

	public float secondsBetweenSpawns;
	public float secondsBeforeStart;
	public GameObject[] attackPrefabs;

	private Timer spawnTimer;
	private Timer startTimer;

    public bool started;

	// Use this for initialization
	void Start () {
        started = false;
        startTimer = new Timer(secondsBeforeStart);
		spawnTimer = new Timer(secondsBetweenSpawns);
	}

	// Update is called once per frame
	void Update () {
        Debug.Log("STARTTIMER " + startTimer.Elapsed());
        Debug.Log("STARTTIMER " + startTimer.Parameterized());

		if(started && spawnTimer.Finished()) {
			GameObject toSpawn = attackPrefabs[Random.Range(0, attackPrefabs.Length)];
			Instantiate(toSpawn, Vector3.zero, Quaternion.identity);

			spawnTimer.Start();
		}
	}

    public void StartSpawning(){
        started = true;
        startTimer = new Timer(secondsBeforeStart);
    	startTimer.Start();
    }
}
