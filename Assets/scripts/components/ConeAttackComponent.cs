using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeAttackComponent : MonoBehaviour {

	public GameObject bulletPrefab;
	public int bulletsPerSecond;
	public float minFireSpreadInDeg;
	public float maxFireSpreadInDeg;
	public float fireOffsetAngle;
	public float damage;
	public float durationInSeconds;

	private float secondsBetweenBullets;
	private Timer finishTimer;
	private float lastFireTime;

	private GameObject enemyGameObject;
	
	// Use this for initialization
	void Start () {
		finishTimer = new Timer(durationInSeconds);
		finishTimer.Start();
		
		secondsBetweenBullets = 1.0f / bulletsPerSecond;
		
		lastFireTime = Time.time - secondsBetweenBullets;
		
		enemyGameObject = GameObject.FindWithTag("Enemy");

	}
	
	// Update is called once per frame
	void Update () {
		if (!finishTimer.Finished() && IsTimeToFire()) {
			float fireSpreadInDeg = minFireSpreadInDeg + finishTimer.Parameterized() * (maxFireSpreadInDeg - minFireSpreadInDeg);
			
			GameObject bullet = Instantiate(bulletPrefab);
			bullet.transform.position = enemyGameObject.transform.position;
			float angle = Random.Range(-fireSpreadInDeg / 2.0f, fireSpreadInDeg / 2.0f) + fireOffsetAngle;
			bullet.transform.Rotate(new Vector3(0, angle + 180, 0));
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
