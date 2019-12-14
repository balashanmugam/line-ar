using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Singleton<HUD> {
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;


    [SerializeField] private GameObject score;
    [SerializeField] private GameObject ready;

    [SerializeField] private Button startButton;
    [SerializeField] private Button powerUpButton;

    private void OnEnable() {
        startButton.onClick.AddListener(StartGame);
    }

    private void OnDisable() {
        startButton.onClick.AddListener(StartGame);
    }

    public void ToggleReady(bool state) {
        ready.SetActive(state);
        ToggleScoreAndPower(!state);
    }

    public void ToggleScoreAndPower(bool state) {
        score.gameObject.SetActive(state);
        powerUpButton.gameObject.SetActive(state);
    }

    public void TogglePowerUp(bool state) {
        powerUpButton.interactable = state;
    }

    public void StartGame() {
        ToggleReady(false);
        GameManager.Instance.PlayGame();
    }
}