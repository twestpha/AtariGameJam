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
	
	// Use this for initialization
	void Start () {
		finishTimer = new Timer(durationInSeconds);
		finishTimer.Start();
		
		secondsBetweenBullets = 1.0f / bulletsPerSecond;
		
		lastFireTime = Time.time - secondsBetweenBullets;

	}
	
	// Update is called once per frame
	void Update () {
		if (!finishTimer.Finished() && IsTimeToFire()) {
			float fireSpreadInDeg = minFireSpreadInDeg + finishTimer.Parameterized() * (maxFireSpreadInDeg - minFireSpreadInDeg);
			
			GameObject bullet = Object.Instantiate(bulletPrefab);
			bullet.transform.position = transform.position;
			float angle = Random.Range(-fireSpreadInDeg / 2.0f, fireSpreadInDeg / 2.0f) + fireOffsetAngle;
			bullet.transform.Rotate(new Vector3(0, angle + 180, 0));
			bullet.GetComponent<ProjectileComponent>().damage = damage;
			bullet.GetComponent<DamagingComponent>().creator = GameObject.FindWithTag("Enemy");
			lastFireTime = Time.time;
		}
	}

	bool IsTimeToFire() {
		return Time.time - lastFireTime >= secondsBetweenBullets;
	}
}
