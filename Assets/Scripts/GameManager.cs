using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Markup;
using LineAR;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject EnemyPrefab;

    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;

    [SerializeField] private Transform _spawnPointParent;
    [SerializeField] private List<Transform> spawnPoint;

    [SerializeField] private int randomPoint;
    [SerializeField] private int randomPath;

    [SerializeField] private List<bool> spawnBools;

    [SerializeField] private int enemiesCount = 1;

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

    private void Start() {
        for (int i = 0; i < _spawnPointParent.childCount; i++) {
            spawnPoint.Add(_spawnPointParent.transform.GetChild(i));
            spawnBools.Add(false);
        }

        // Spawn Player
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy() {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i <= enemiesCount; i++) {
            bool isUnique = false;

            while (!isUnique) {
                randomPoint = Random.Range(0, _spawnPointParent.childCount);
                if(spawnBools[randomPoint] == true) continue;
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
    }
}