using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotationComponent : MonoBehaviour {

    public float rotationSpeed;

	void Start(){

	}

	void Update(){
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
	}
}
