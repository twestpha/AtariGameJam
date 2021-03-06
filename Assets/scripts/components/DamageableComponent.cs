﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageableComponent : MonoBehaviour {

    public float currentHealth;
    public float maxHealth;
    public bool invincible;
    public bool destroySelfOnDeath;

    public AudioSource damageableAudioSource;

    public GameObject collisionEffectPrefab;

    public DamagingComponent.Team team;

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

            if(damaging && damaging.creator == gameObject || team == damaging.team){
                return;
            }

            if(!invincible){
                currentHealth -= damaging.damage;

                if(damageableAudioSource && !damageableAudioSource.isPlaying && currentHealth > 0.0f){
                    damageableAudioSource.Play();
                }

                if(gameObject.tag == "Player" && currentHealth > 0.0f){
                    Camera.main.GetComponent<CameraShakeComponent>().AddMediumShake();
                }
            }

            if(damaging.destroyOnDamageDone){
                Destroy(other.gameObject);
            }

            if(collisionEffectPrefab){
                GameObject collfx = Object.Instantiate(collisionEffectPrefab);
                collfx.transform.position = (other.transform.position - transform.position) * 0.5f + transform.position;
            }

            if(destroySelfOnDeath && currentHealth <= 0.0f){
                Destroy(gameObject);
            }
        }
    }

    public float getMaxHealth() {
        return maxHealth;
    }
}
