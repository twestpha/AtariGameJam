using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickupSpawner : MonoBehaviour {

    public float xrange;
    public float yrange;

    public float spawnCooldown;
    private Timer spawnTimer;

    public GameObject shieldPickupPrefab;

    void Start(){
        spawnTimer = new Timer(spawnCooldown);
        spawnTimer.Start();
    }

    void Update(){
        if(spawnTimer.Finished()){
            GameObject pickup = Object.Instantiate(shieldPickupPrefab);
            pickup.transform.position = new Vector3(Random.Range(-xrange, xrange), 0.0f, Random.Range(-yrange, yrange));
            spawnTimer.Start();
        }
    }
}
