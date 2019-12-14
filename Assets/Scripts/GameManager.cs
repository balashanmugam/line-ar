using System.Collections;
using System.Collections.Generic;
using LineAR;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject EnemyPrefab;

    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;


    [SerializeField] private Transform _spawnPointParent;
    private Transform ground;
    [SerializeField] private List<Transform> spawnPoint;

    [SerializeField] private int randomPoint;
    [SerializeField] private List<bool> spawnBools;

    [SerializeField] private int enemiesCount = 1;

    [SerializeField] private int enemyLostCount = 0;


    public Transform SpawnPointParent
    {
        get => _spawnPointParent;
        set => _spawnPointParent = value;
    }

    public List<Transform> SpawnPoint
    {
        get => spawnPoint;
        set => spawnPoint = value;
    }

    public List<bool> SpawnBools
    {
        get => spawnBools;
        set => spawnBools = value;
    }

    public int EnemyLostCount
    {
        get => enemyLostCount;
        set {
            enemyLostCount = value;
            if (enemyLostCount == (enemiesCount + 1)) {
                WinGame();
            }
        }
    }

    private void Start() {
        ground = _spawnPointParent.parent;
        for (int i = 0; i < _spawnPointParent.childCount; i++) {
            spawnPoint.Add(_spawnPointParent.transform.GetChild(i));
            spawnBools.Add(false);
        }
#if UNITY_EDITOR_OSX || UNITY_EDITOR
        // Testing code.
        BeginGame();
#endif
    }

    public void WinGame() {
        StartCoroutine(Win());
    }

    IEnumerator Win() {
        HUD.Instance.ToggleScoreAndPower(false);

        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.ToggleEndGame(true);
        UIManager.Instance.SetEndMessage("You are Victorious!", "high");
        Time.timeScale = 0;
    }

    IEnumerator Lose() {
        HUD.Instance.ToggleScoreAndPower(false);

        yield return new WaitForSeconds(1.5f);

        UIManager.Instance.ToggleEndGame(true);
        UIManager.Instance.SetEndMessage("You were defeated!", "low");

        Time.timeScale = 0;
    }

    public void DefeatedGame() {
        // call after a small delay
        StartCoroutine(Lose());
    }

    public void BeginGame() {
        // Spawn Player 
        StartCoroutine(SpawnCharacters());
        HUD.Instance.ToggleReady(true);
    }

    public void PlayGame() {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame() {
        yield return new WaitForSeconds(2f);

        if (player != null)
            player.GetComponent<MeshGenerator>().StartGrow = true;

        if (enemy1 != null)
            enemy1.GetComponent<MeshGenerator>().StartGrow = true;
        if (enemy2 != null)
            enemy2.GetComponent<MeshGenerator>().StartGrow = true;
        if (enemy3 != null)
            player.GetComponent<MeshGenerator>().StartGrow = true;
    }

    IEnumerator SpawnCharacters() {
        yield return new WaitForSeconds(0.1f);
        bool isUnique = false;

        // Spawn enemies
        for (int i = 0; i <= enemiesCount; i++) {
            isUnique = false;
            while (!isUnique) {
                randomPoint = Random.Range(0, _spawnPointParent.childCount);
                if (spawnBools[randomPoint] == true) continue;
                else {
                    isUnique = true;
                    spawnBools[randomPoint] = true;
                }
            }

            switch (i) {
                case 0:
                    enemy1 = Instantiate(EnemyPrefab, spawnPoint[randomPoint].position,
                        spawnPoint[randomPoint].rotation);
                    enemy1.GetComponent<EnemyBot>().PathParent = spawnPoint[randomPoint];
                    break;
                case 1:
                    enemy2 = Instantiate(EnemyPrefab, spawnPoint[randomPoint].position,
                        spawnPoint[randomPoint].rotation);
                    enemy2.GetComponent<EnemyBot>().PathParent = spawnPoint[randomPoint];
                    break;
                case 2:
                    enemy3 = Instantiate(EnemyPrefab, spawnPoint[randomPoint].position,
                        spawnPoint[randomPoint].rotation);
                    enemy3.GetComponent<EnemyBot>().PathParent = spawnPoint[randomPoint];
                    break;
            }
        }

        //Spawn player

        isUnique = false;

        while (!isUnique) {
            randomPoint = Random.Range(0, _spawnPointParent.childCount);
            if (spawnBools[randomPoint] == true) continue;
            else {
                isUnique = true;
                spawnBools[randomPoint] = true;
            }
        }

        player = Instantiate(playerPrefab, spawnPoint[randomPoint].position,
            Quaternion.Euler(spawnPoint[randomPoint].rotation.eulerAngles));
    }
}