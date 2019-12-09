using System.Collections;
using System.Collections.Generic;
using LineAR;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject EnemyPrefab;

    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;

    [SerializeField] private Transform _spawnPointParent;
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
            if (enemyLostCount == (enemiesCount + 1) ){
                WinGame();
            }
        }
    }

    private void Start() {
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
        UIManager.Instance.ToggleEndGame(true);
        UIManager.Instance.SetEndMessage("You are Victorious!", "high");
        Time.timeScale = 0;
    }

    public void DefeatedGame() {
        UIManager.Instance.ToggleEndGame(true);
        UIManager.Instance.SetEndMessage("You were defeated!", "low");
        Time.timeScale = 0;

    }

    public void BeginGame() {
        // Spawn Player 
        // Call function from Ground plane placement
        StartCoroutine(SpawnCharacters());
        
    }

    IEnumerator StartGame() {
        yield return new WaitForSeconds(3f);

        enemy1.GetComponent<MeshGenerator>().StartGrow = true;
        enemy2.GetComponent<MeshGenerator>().StartGrow = true;
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

        player = Instantiate(playerPrefab, spawnPoint[randomPoint].position, spawnPoint[randomPoint].rotation);

        StartCoroutine(StartGame());
    }
}