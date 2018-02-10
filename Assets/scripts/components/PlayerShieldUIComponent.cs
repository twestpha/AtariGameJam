using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerShieldUIComponent : MonoBehaviour {

	public GameObject player;

	private PlayerComponent playerComponent;
	// Use this for initialization
	void Start () {
		playerComponent = player.GetComponent<PlayerComponent>();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Slider>().value = playerComponent.shields / playerComponent.maxShields;
	}
}
