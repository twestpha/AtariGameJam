using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawnerComponent : MonoBehaviour {

	public int numberOfMissiles;
	public GameObject missilePrefab;
	
	// Use this for initialization
	void Start () {
		GameObject enemy = GameObject.FindWithTag("Enemy");
		for (int i = 0; i < numberOfMissiles; i++) {
			GameObject missile = Instantiate(missilePrefab, enemy.transform.position, missilePrefab.transform.rotation);
			missile.GetComponent<MissileComponent>().driftTime = Random.Range(1.0f, 3.0f);
			missile.GetComponent<DamagingComponent>().creator = enemy;
		}
		
		Destroy(gameObject);
	}
}
