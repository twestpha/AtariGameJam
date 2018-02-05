using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileComponent : MonoBehaviour {

    public float driftTime;
    public float driftSpeed;
    public float fireTime;
    public float fireSpeed;

    public Vector3 velocity;

    public GameObject particles;

    public float destroyDistance;

    enum State {
        drifting,
        firing,
    }

    private GameObject player;
    private Vector3 playerVector;
    private Vector3 startPosition;

    private State state;
    private Timer driftTimer;
    private Timer fireTimer;

    void Start(){
        state = State.drifting;
        player = GameObject.FindWithTag("Player");
        driftTimer = new Timer(driftTime);
        driftTimer.Start();

        fireTimer = new Timer(fireTime);

        velocity = new Vector3(-Random.value / 2.0f, 0.0f, Random.value - 0.5f).normalized * driftSpeed;
        startPosition = transform.position;
    }

    void Update(){
        if(state == State.drifting && driftTimer.Finished()){
            state = State.firing;
            fireTimer.Start();
            playerVector = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(playerVector);
            particles.GetComponent<ParticleSystem>().Play();
        }
        else if(state == State.firing){
            velocity = playerVector * fireSpeed * fireTimer.Parameterized();
        }

        transform.position += velocity * Time.deltaTime;

        if((transform.position - startPosition).magnitude > destroyDistance){
            Destroy(gameObject);
        }
    }
}
