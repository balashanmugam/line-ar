using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EndGameScript : MonoBehaviour {
    [SerializeField] private Text scoreText;
    [SerializeField] private Text messageText;

    [SerializeField] private Button restartButton, mainMenuButton, quitButton;

    public void SetMessage(string message, string score) {
        scoreText.text = score;
        messageText.text = message;
    }
    
    private void OnEnable() {
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(MainMenuStart);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable() {
        restartButton.onClick.RemoveListener(RestartGame);
        mainMenuButton.onClick.RemoveListener(MainMenuStart);
        quitButton.onClick.RemoveListener(QuitGame);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuStart()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
