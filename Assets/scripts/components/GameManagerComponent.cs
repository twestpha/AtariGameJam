using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerComponent : MonoBehaviour {

	public GameObject player;
	public GameObject enemy;

	public GameObject winUI;
	public GameObject loseUI;

	public Text scoreText;

	public Text loseScoreText;

	public Text winBaseScoreText;
	public Text winDamageTakenScoreText;
	public Text winTimeBonusScoreText;
	public Text winFinalScoreText;

    public float slowdownTime;
    private Timer slowdownTimer;

    private Timer restartTimer;
    private bool playerDied;

    private float prevplayerhealth;
    private float prevenemyhealth;

	private Timer gameTimer;
	private bool alreadyCalculatedScoreBonus;
	private bool won;
	
	// Use this for initialization
	void Start () {
		winUI.SetActive(false);
		loseUI.SetActive(false);

        slowdownTimer = new Timer(slowdownTime);
        restartTimer = new Timer(1.6f);
		
		gameTimer = new Timer();
		gameTimer.Start();
	}

	// Update is called once per frame
	void Update () {
		scoreText.text = "SCORE: " + (int)calculateScore();

        float enemyhealth = enemy.GetComponent<DamageableComponent>().currentHealth;
        float playerhealth = player.GetComponent<DamageableComponent>().currentHealth;

        if(prevenemyhealth > 0.0f && enemyhealth <= 0.0f){
            slowdownTimer.Start();
            restartTimer.Start();
	        // Don't restart when you win
            // playerDied = true;
        }

        if(prevplayerhealth > 0.0f && playerhealth <= 0.0f && !won){
            slowdownTimer.Start();
            restartTimer.Start();
            playerDied = true;
        }

		if(playerhealth <= 0.0f && !won) {
			loseScoreText.text = "FINAL SCORE: " + calculateScore();
			scoreText.enabled = false;
			loseUI.SetActive(true);
			Time.timeScale = 1.05f - slowdownTimer.Parameterized();
		}

		if(enemyhealth <= 0.0f) {
			won = true;
			if (!alreadyCalculatedScoreBonus) {
				alreadyCalculatedScoreBonus = true;
				setupWinUI();
			}
			Time.timeScale = 1.05f - slowdownTimer.Parameterized();
		}

        prevenemyhealth = enemyhealth;
        prevplayerhealth = playerhealth;

        if(restartTimer.Finished() && playerDied){
	        Debug.Log("Realoding scene");
	        Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
		
	}

	float calculateScore() {
		DamageableComponent enemyDamageable = enemy.GetComponent<DamageableComponent>();
		
		float baseScore = enemyDamageable.maxHealth - enemyDamageable.currentHealth;
		return baseScore + (player.GetComponent<PlayerComponent>().GetNumberOfShieldsPickedUp() * 50.0f);
	}

	float getPlayerDamageTaken() {
		DamageableComponent playerDamageable = player.GetComponent<DamageableComponent>();
		return playerDamageable.maxHealth - playerDamageable.currentHealth;
	}
	
	float calculateTimeBonus() {
		float totalTime = gameTimer.Elapsed();
		float timeBonus;
		if (totalTime < 80.0f) {
			timeBonus = 500.0f;
		} else if (totalTime < 100.0f) {
			timeBonus = 250.0f;
		} else if (totalTime < 120.0f) {
			timeBonus = 150.0f;
		}
		else {
			timeBonus = 50.0f;
		}
		return timeBonus;
	}

	void setupWinUI() {
		scoreText.enabled = false;
		
		float baseScore = calculateScore();
		float damageTaken = getPlayerDamageTaken();
		float timeBonus = calculateTimeBonus();
		float finalScore = calculateScore() + calculateTimeBonus() - getPlayerDamageTaken();

		winBaseScoreText.text        = "BASE SCORE:            " + baseScore;
		winDamageTakenScoreText.text = "DAMAGE TAKEN PENALTY: -" + damageTaken;
		winTimeBonusScoreText.text   = "TIME BONUS:           +" + timeBonus;
		winFinalScoreText.text       = "FINAL SCORE:           " + finalScore;
		
		winUI.SetActive(true);

	}
	
}
