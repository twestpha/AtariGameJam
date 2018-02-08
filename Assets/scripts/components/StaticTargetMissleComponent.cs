using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTargetMissleComponent : MonoBehaviour {
    public float speed;
    public float secondsBeforeStarting;
    public float acceleration;
    
    private Vector3 target;
    private float minDestroyDistance = 0.35f;
    private Timer startTimer;
    
    public GameObject collisionEffectPrefab;
    
    public void SetTarget(Vector3 target) {
        this.target = target;
    }

    void Start() {
        startTimer = new Timer(secondsBeforeStarting);
        startTimer.Start();
    }

    void Update() {
        if (startTimer.Finished()) {
            Vector3 playerVector = target - transform.position;
            playerVector.y = 0.0f;
            playerVector.Normalize();
            speed = speed + acceleration;
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