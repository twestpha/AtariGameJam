using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTargetMissleComponent : MonoBehaviour {
    public float speed;
    
    private Vector3 target;
    private bool isActive;
    private float minDestroyDistance = 0.05f;

    public GameObject collisionEffectPrefab;
    
    public void SetTarget(Vector3 target) {
        this.target = target;
    }

    public void Activate() {
        isActive = true;
    }

    void Update() {
        if (isActive) {
            Vector3 playerVector = target - transform.position;
            playerVector.y = 0.0f;
            playerVector.Normalize();
            Vector3 velocity = playerVector * speed;
            transform.position += velocity * Time.deltaTime;
            
            if((transform.position - target).magnitude < minDestroyDistance){
                if(collisionEffectPrefab){
                    GameObject collfx = Object.Instantiate(collisionEffectPrefab);
                    collfx.transform.position = transform.position;
                }
                Destroy(gameObject);
            }
        }
    }
}