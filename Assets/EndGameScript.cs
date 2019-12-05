using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public Text ScoreText;
    public GameObject MainMenuScreen;
    public void RestartGame()
    {
        SceneManager.LoadScene("mesh-test");
    }

    public void MainMenuStart()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
