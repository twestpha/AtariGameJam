using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class DamagingComponent : MonoBehaviour {

    public const int DAMAGING_LAYER = 8;

    public float damage;
    public bool destroyOnDamageDone;

	void Start(){
        if(gameObject.layer != DAMAGING_LAYER){
            Debug.LogWarning("Damaging Component requires to be layer " + DAMAGING_LAYER + ", but currently is " + gameObject.layer);
            gameObject.layer = DAMAGING_LAYER;
        }

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if(!rigidbody.isKinematic){
            Debug.LogWarning("Damaging Component requires kinematic rigidbody");
            rigidbody.isKinematic = true;
        }
        if(rigidbody.interpolation != RigidbodyInterpolation.Interpolate){
            Debug.LogWarning("Damaging Component requires interpolating rigidbody");
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }
        if(rigidbody.collisionDetectionMode != CollisionDetectionMode.Continuous){
            Debug.LogWarning("Damaging Component requires continuous collision detection rigidbody");
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
	}

	void Update(){

	}
}
