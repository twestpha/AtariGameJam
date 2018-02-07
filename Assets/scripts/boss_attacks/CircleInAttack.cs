using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class CircleInAttack : MonoBehaviour {

	public GameObject bulletPrefab;
	public float startingRadius;
	public int numberOfBullets;
	public float warningTimeSeconds;

	private Timer warningTimer;

	private GameObject[] bullets;
	private bool bulletsActivated = false;
	
	// Use this for initialization
	void Start () {
		Vector3 centerTarget = GameObject.FindWithTag("Player").transform.position;

		bullets = new GameObject[numberOfBullets];
		for (int i = 0; i < numberOfBullets; i++) {
			float angleInRad = (2.0f * Mathf.PI * i) / numberOfBullets;
			float x = Mathf.Cos(angleInRad) * startingRadius + centerTarget.x;
			float y = Mathf.Sin(angleInRad) * startingRadius + centerTarget.y;
			GameObject bullet = Instantiate(bulletPrefab, new Vector3(x, 0, y), Quaternion.identity); 
			bullet.SendMessage("SetTarget", centerTarget);
			bullets[i] = bullet;
		}
		
		warningTimer = new Timer(warningTimeSeconds);
		warningTimer.Start();
	}
	
	// Update is called once per frame
	void Update () {
		if (!bulletsActivated && warningTimer.Finished()) {
			Debug.Log("Finished warning, time to die!");
			for (int i = 0; i < numberOfBullets; i++) {
				// Some bullets might not be there because the player can run into them
				bullets[i].SendMessage("Activate");
			}

			bulletsActivated = true;
		}
	}
}
