using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class DamageableHealthUIComponent : MonoBehaviour {

	public GameObject damageable;

	private DamageableComponent damageableComponent;
	// Use this for initialization
	void Start () {
		damageableComponent = damageable.GetComponent<DamageableComponent>();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Slider>().value = damageableComponent.currentHealth / damageableComponent.maxHealth;	
	}
}
