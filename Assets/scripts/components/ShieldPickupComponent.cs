using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickupComponent : MonoBehaviour {

    public float pickupAmount;
    public float shrinkRate;

	void Start(){

	}

	void Update(){
        float newScale = transform.localScale.x - shrinkRate * Time.deltaTime;
        transform.localScale = new Vector3(newScale, newScale, newScale);

        if(newScale < 0.0f){
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerComponent>().shields += pickupAmount;
            other.gameObject.GetComponent<PlayerComponent>().PlayShieldPickupSound();
            Destroy(gameObject);
        }
    }
}
