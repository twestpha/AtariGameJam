using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTargetMissleComponent : MonoBehaviour {
    public float speed;
    public float secondsBeforeStarting;
    public float acceleration;

    private Vector3 target;
    private float minDestroyDistance = 1.0f;
    private Timer startTimer;

    private Timer maxBullshitTimer;

    public GameObject collisionEffectPrefab;

    Vector3 originalTargetVector;

    public void SetTarget(Vector3 target) {
        this.target = target;
        originalTargetVector = target - transform.position;
    }

    void Start() {
        startTimer = new Timer(secondsBeforeStarting);
        maxBullshitTimer = new Timer(4.0f);
        startTimer.Start();
        maxBullshitTimer.Start();
    }

    void Update() {
        if (startTimer.Finished()) {
            Vector3 playerVector = target - transform.position;
            playerVector.y = 0.0f;
            playerVector.Normalize();
            speed = speed + acceleration;
            Vector3 velocity = playerVector * speed;
            transform.position += velocity * Time.deltaTime;

            if((transform.position - target).magnitude < minDestroyDistance ||
                originalTargetVector.normalized == -1.0f * velocity.normalized || maxBullshitTimer.Finished()){
                if(collisionEffectPrefab){
                    GameObject collfx = Object.Instantiate(collisionEffectPrefab);
                    collfx.transform.position = transform.position;
                }
                Destroy(gameObject);
            }
        }
    }
}
