using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttackComponent : MonoBehaviour {

	public GameObject projectilePrefab;
	public int numberOfWaves;
	public float secondsBetweenWaves;
	
	private Timer spawnTimer;
	private GameObject enemy;

	private float[] possibleZs;
	private int wavesCompleted;
	
	// Use this for initialization
	void Start () {
		enemy = GameObject.FindWithTag("Enemy");

		possibleZs = new float[] {14, 7, 0, -7, -14};
		
		spawnTimer = new Timer(secondsBetweenWaves);
		spawnTimer.Start();
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer.Finished()) {
			wavesCompleted += 1;
			spawnThings();
			spawnTimer = new Timer(secondsBetweenWaves);
			spawnTimer.Start();
		}

		if (wavesCompleted == numberOfWaves) {
			Destroy(gameObject);
		}
			
	}

	void spawnThings() {
		int skipIndex = Random.Range(0, 5);
		for (int i = 0; i < possibleZs.Length; i++) {
			if (i != skipIndex) {
				GameObject projectile = Instantiate(
					projectilePrefab, 
					new Vector3(enemy.transform.position.x + 7, 0, possibleZs[i]), 
					projectilePrefab.transform.rotation);
				projectile.GetComponent<DamagingComponent>().creator = enemy;

			}
		}
	}
}
