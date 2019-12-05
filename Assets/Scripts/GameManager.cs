using System.Collections;
using System.Collections.Generic;
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

    private void Start() {
        for (int i = 0; i < _spawnPointParent.childCount; i++) {
            spawnPoint.Add(_spawnPointParent.transform.GetChild(i));
        }

        randomPoint = 0;//Random.Range(0, _spawnPointParent.childCount );

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy() {
        yield return new WaitForSeconds(0.1f);

        enemy1 = Instantiate(EnemyPrefab, spawnPoint[randomPoint].position, spawnPoint[randomPoint].rotation);
    }
    
}