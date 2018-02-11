using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalProjectileComponent : MonoBehaviour {

	public float speed = 0.0f;
	public float destroyDistance = 100.0f;
	public Vector3 direction;

	public float damage;

	private Vector3 startPosition;

	void Start(){
		startPosition = transform.position;
	}

	void Update(){
		transform.position += direction * speed * Time.deltaTime;

		if((transform.position - startPosition).magnitude > destroyDistance){
			Destroy(gameObject);
		}
	}
}
