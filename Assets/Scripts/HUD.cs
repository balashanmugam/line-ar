using System;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class HUD : Singleton<HUD> {
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Text timerText;


    [SerializeField] private GameObject score;
    [SerializeField] private GameObject ready;

    [SerializeField] private Button startButton;
    [SerializeField] private Button powerUpButton;

    [SerializeField] private float timer = 0;

    [SerializeField] private PlayerInput _playerInput;

    public float Timer
    {
        get => timer;
        set => timer = value;
    }

    public PlayerInput PlayerInput
    {
        get => _playerInput;
        set => _playerInput = value;
    }


    private void OnEnable() {
        startButton.onClick.AddListener(StartGame);
    }

    private void OnDisable() {
        startButton.onClick.RemoveListener(StartGame);
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

    public void Update() {
        if (!_playerInput.Player.StartGrow) return;
        
        timer += (Time.deltaTime * 10);
        scoreText.text = $"SCORE: {timer:0.00}";
    }

    private void StartGame() {
        ToggleReady(false);
        GameManager.Instance.PlayGame();
    }
}