using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttackComponent : MonoBehaviour {

	public GameObject bulletPrefab;
	public float startingRadius;
	public int numberOfBullets;

	// Use this for initialization
	void Start () {
		Vector3 centerTarget = GameObject.FindWithTag("Player").transform.position;
	
		for (int i = 0; i < numberOfBullets; i++) {
			float angleInRad = (2.0f * Mathf.PI * i) / numberOfBullets;
			float x = Mathf.Cos(angleInRad) * startingRadius + centerTarget.x;
			float y = Mathf.Sin(angleInRad) * startingRadius + centerTarget.z;
			GameObject bullet = Instantiate(bulletPrefab, new Vector3(x, 0, y), Quaternion.identity); 
			bullet.GetComponent<DamagingComponent>().creator = GameObject.FindWithTag("Enemy");
			bullet.GetComponent<StaticTargetMissleComponent>().SetTarget(centerTarget);
		}
		
		Destroy(gameObject);
	}
}
