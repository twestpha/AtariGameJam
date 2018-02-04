using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileComponent : MonoBehaviour {

    public float driftTime;
    public float driftSpeed;
    public float fireTime;
    public float fireSpeed;

    public Vector3 velocity;

    enum State {
        drifting,
        firing,
    }

    private GameObject player;
    private Vector3 playerVector;

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
    }

    void Update(){
        if(state == State.drifting && driftTimer.Finished()){
            state = State.firing;
            fireTimer.Start();
            playerVector = (player.transform.position - transform.position).normalized;
        }
        else if(state == State.firing){
            velocity = playerVector * fireSpeed * fireTimer.Parameterized();
        }

        transform.position += velocity * Time.deltaTime;
    }
}
