﻿using System;
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
	public Text timeText;

	public Text loseBaseScoreText;
	public Text loseDamageTakenScoreText;
	public Text loseScoreText;

	public Text winBaseScoreText;
	public Text winDamageTakenScoreText;
	public Text winTimeBonusScoreText;
	public Text winFinalScoreText;

	public float minEnemyAttackSpawnTime;

    public float slowdownTime;
    private Timer slowdownTimer;

    private Timer restartTimer;
    private bool playerDied;

    private float prevplayerhealth;
    private float prevenemyhealth;

	private Timer gameTimer;
	private bool calculatedFinalScore;
	private bool won;

	private bool startedGameTimer;
	private float startingEnemyAttackSpawnTime;

	// Use this for initialization
	void Start () {
		winUI.SetActive(false);
		loseUI.SetActive(false);

        slowdownTimer = new Timer(slowdownTime);
        restartTimer = new Timer(1.6f);

		gameTimer = new Timer();

		startingEnemyAttackSpawnTime = enemy.GetComponent<RandomSpawnerComponent>().secondsBetweenSpawns;
	}

	public void StartGameTimer() {
		if (!startedGameTimer) {
			gameTimer.Start();
			startedGameTimer = true;
		}
	}

	// Update is called once per frame
	void Update () {
		scoreText.text = "SCORE: " + (int)calculateScore();
		if (!calculatedFinalScore && startedGameTimer) {
			timeText.text = "TIME:  " + gameTimer.Elapsed().ToString("F2");
		}

		enemy.GetComponent<RandomSpawnerComponent>().secondsBetweenSpawns = calculateBossAttackSpawnTime();

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
			if (!calculatedFinalScore) {
				calculatedFinalScore = true;
				setupLoseUI();
			}
			Time.timeScale = 1.05f - slowdownTimer.Parameterized();
		}

		if(enemyhealth <= 0.0f) {
			won = true;
			if (!calculatedFinalScore) {
				calculatedFinalScore = true;
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

	float calculateBossAttackSpawnTime() {
		if (startedGameTimer) {
			float t = Mathf.Min(1, gameTimer.Elapsed() / 60.0f);
			return Mathf.Lerp(startingEnemyAttackSpawnTime, minEnemyAttackSpawnTime, t);
		} else {
			return startingEnemyAttackSpawnTime;
		}

	}

	float calculateScore() {
		DamageableComponent enemyDamageable = enemy.GetComponent<DamageableComponent>();

		float baseScore = enemyDamageable.maxHealth - enemyDamageable.currentHealth;
		return baseScore + (player.GetComponent<PlayerComponent>().GetNumberOfShieldsPickedUp() * 25.0f);
	}

	float getPlayerDamageTaken() {
		DamageableComponent playerDamageable = player.GetComponent<DamageableComponent>();
		return playerDamageable.maxHealth - playerDamageable.currentHealth;
	}

	float calculateTimeBonus() {
		float totalTime = gameTimer.Elapsed();

		if (totalTime <= 80f) {
			float bonus = Mathf.Pow(1.2f, 80f - totalTime);
			return Mathf.Round(1000f * bonus); 
		} else if (totalTime <= 90f) {
			float t = (90f - totalTime) / 10f;
			return Mathf.Round(Mathf.Lerp(500f, 750f, t));
		} else if (totalTime <= 100.0f) {
			return 250f;
		} else if (totalTime < 120f) {
			return 150f;
		} else {
			return 50f;
		}
	}

	void setupLoseUI() {
		float baseScore = calculateScore();
		float damageTaken = getPlayerDamageTaken();
		float finalScore = Mathf.Max(baseScore - damageTaken, 0);
		loseBaseScoreText.text        = "BASE SCORE:            " + baseScore;
		loseDamageTakenScoreText.text = "DAMAGE TAKEN PENALTY: -" + damageTaken;
		loseScoreText.text            = "FINAL SCORE:           " + finalScore;
		scoreText.enabled = false;
		loseUI.SetActive(true);
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
