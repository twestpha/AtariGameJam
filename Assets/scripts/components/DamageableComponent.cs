﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageableComponent : MonoBehaviour {

    public float currentHealth;

    public GameObject collisionEffectPrefab;

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

            if(damaging && damaging.creator == gameObject){
                return;
            }

            currentHealth -= damaging.damage;

            if(collisionEffectPrefab){
                GameObject collfx = Object.Instantiate(collisionEffectPrefab);
                collfx.transform.position = (other.transform.position - transform.position) * 0.5f + transform.position;
            }

            if(damaging.destroyOnDamageDone){
                Destroy(other.gameObject);
            }
        }
    }
}