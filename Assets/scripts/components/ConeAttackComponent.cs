using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeAttackComponent : MonoBehaviour {

	public GameObject bulletPrefab;
	public int bulletsPerSecond;
	public float minFireSpreadInDeg;
	public float maxFireSpreadInDeg;
	public float damage;
	public float durationInSeconds;

	private float secondsBetweenBullets;
	private Timer finishTimer;
	private float lastFireTime;

	private GameObject enemyGameObject;
	private float angleToPlayerInDeg;
	
	// Use this for initialization
	void Start () {
		finishTimer = new Timer(durationInSeconds);
		finishTimer.Start();
		
		secondsBetweenBullets = 1.0f / bulletsPerSecond;
		
		lastFireTime = Time.time - secondsBetweenBullets;
		
		enemyGameObject = GameObject.FindWithTag("Enemy");

		Vector3 enemyToPlayer = enemyGameObject.transform.position - GameObject.FindWithTag("Player").transform.position;
		angleToPlayerInDeg = Quaternion.LookRotation(enemyToPlayer).eulerAngles.y + 90;
	}
	
	// Update is called once per frame
	void Update () {
		if (!finishTimer.Finished() && IsTimeToFire()) {
			GameObject bullet = Instantiate(bulletPrefab);

			bullet.transform.position = enemyGameObject.transform.position;


			float fireSpreadInDeg = minFireSpreadInDeg + finishTimer.Parameterized() * (maxFireSpreadInDeg - minFireSpreadInDeg);
			float angle = Random.Range(-fireSpreadInDeg / 2.0f, fireSpreadInDeg / 2.0f) + angleToPlayerInDeg;
			bullet.transform.Rotate(new Vector3(0, angle , 0));
			
			bullet.GetComponent<ProjectileComponent>().damage = damage;
			bullet.GetComponent<DamagingComponent>().creator = enemyGameObject;
			
			lastFireTime = Time.time;
		} else if (finishTimer.Finished()) {
			Destroy(gameObject);
		}
	}

	bool IsTimeToFire() {
		return Time.time - lastFireTime >= secondsBetweenBullets;
	}
}
