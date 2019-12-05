using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingscreen;
    public Slider slider;
    public Text progressText;

    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("menu");
        if (objects.Length > 1)
            Destroy(this.gameObject); // we do this because when we shift from the mesh scene back to the men, another gameobject gets created and the music becomes unstable.
        DontDestroyOnLoad(this.gameObject); //not to destroy this object as the scene changes

    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        this.gameObject.SetActive(false);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingscreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = progress*100f + "%";
            yield return null;
        }

    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("game has ended.");
    }
}