using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerComponent : MonoBehaviour {

	public GameObject player;
	public GameObject enemy;
	
	public GameObject winUI;
	public GameObject loseUI;
	
	// Use this for initialization
	void Start () {
		winUI.SetActive(false);
		loseUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<DamageableComponent>().currentHealth <= 0.0f) {
			loseUI.SetActive(true);
			Time.timeScale = 0.0f;
		}

		if (enemy.GetComponent<DamageableComponent>().currentHealth <= 0.0f) {
			winUI.SetActive(true);
			Time.timeScale = 0.0f;
		}
	}
}
