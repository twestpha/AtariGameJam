using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour {

    public float speed = 0.0f;
    public float destroyDistance = 100.0f;

    private Vector3 startPosition;

    void Start(){
        startPosition = transform.position;
    }

    void Update(){
        transform.position += transform.forward * speed * Time.deltaTime;

        if((transform.position - startPosition).magnitude > destroyDistance){
            Destroy(gameObject);
        }
    }
}
