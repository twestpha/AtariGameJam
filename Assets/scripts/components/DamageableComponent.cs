using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageableComponent : MonoBehaviour {

    public float currentHealth;

	void Start(){
        if(!GetComponent<Collider>().isTrigger){
            GetComponent<Collider>().isTrigger = true;
            Debug.LogWarning("Damageable Component collider needs to be trigger");
        }
	}

	void Update(){

	}

    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == DamagingComponent.DAMAGING_LAYER){
            DamagingComponent damaging = other.gameObject.GetComponent<DamagingComponent>();
            currentHealth -= damaging.damage;

            if(damaging.destroyOnDamageDone){
                Destroy(other.gameObject);
            }
        }
    }
}
