using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotationComponent : MonoBehaviour {

    public float rotationSpeed;
    private float angle;

	void Start(){
        angle = 0.0f;
	}

	void Update(){
        angle += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(1.0f, 0.0f, 0.0f));
	}
}
