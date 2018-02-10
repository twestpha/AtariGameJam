using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerComponent : MonoBehaviour {

	public GameObject player;
	public GameObject enemy;

	public GameObject winUI;
	public GameObject loseUI;

    public float slowdownTime;
    private Timer slowdownTimer;

    private Timer restartTimer;
    private bool playerDied;

    private float prevplayerhealth;
    private float prevenemyhealth;

	// Use this for initialization
	void Start () {
		winUI.SetActive(false);
		loseUI.SetActive(false);

        slowdownTimer = new Timer(slowdownTime);
        restartTimer = new Timer(1.6f);
	}

	// Update is called once per frame
	void Update () {

        float enemyhealth = enemy.GetComponent<DamageableComponent>().currentHealth;
        float playerhealth = player.GetComponent<DamageableComponent>().currentHealth;

        if(prevenemyhealth > 0.0f && enemyhealth <= 0.0f){
            slowdownTimer.Start();
            restartTimer.Start();
            playerDied = true;
        }

        if(prevplayerhealth > 0.0f && playerhealth <= 0.0f){
            slowdownTimer.Start();
            restartTimer.Start();
            playerDied = true;
        }

		if(playerhealth <= 0.0f) {
			loseUI.SetActive(true);
			Time.timeScale = 1.05f - slowdownTimer.Parameterized();
		}

		if(enemyhealth <= 0.0f) {
			winUI.SetActive(true);
			Time.timeScale = 1.05f - slowdownTimer.Parameterized();
		}

        prevenemyhealth = enemyhealth;
        prevplayerhealth = playerhealth;

        if(restartTimer.Finished() && playerDied){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}
}
