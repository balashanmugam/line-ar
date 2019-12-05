using System;
using System.Collections.Generic;
using LineAR;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBot : MonoBehaviour {
    // List of spawn positions
    [SerializeField] private Transform pathParent;
    [SerializeField] private List<Transform> pathPoints;

    // List of target for each spawn location
    [SerializeField] private int currentIndex = 0;

    [SerializeField] private MeshGenerator mesh;

    [SerializeField] private float thresholdDistance = 0.5f;

    [SerializeField] private bool isAlive = true;

    public bool IsAlive
    {
        get => isAlive;
        set {
            if (value == false) {
                mesh.StartGrow = false;
            }
        }
    }


    private void Start() {
        mesh = GetComponent<MeshGenerator>();
    }

    private void OnEnable() {
        // Finding spawn point
        int randomIndex = 0; //Random.Range(0, GameManager.Instance.SpawnPoint.Count);
        pathParent = GameManager.Instance.SpawnPoint[randomIndex];

        int pathRandomIndex = Random.Range(0, pathParent.childCount);
        for (int i = 0; i < pathParent.transform.GetChild(pathRandomIndex).gameObject.transform.childCount; i++) {
            pathPoints.Add(pathParent.transform.GetChild(pathRandomIndex).gameObject.transform.GetChild(i));
        }
    }
    // Follow the path 

    public void Follow() {
        // Look at point

        if (!mesh.StartGrow) return;

        LookAt(pathPoints[currentIndex]);

        if (Vector3.Distance(mesh.Last.transform.position, pathPoints[currentIndex].position) <= thresholdDistance) {
            // Look at the next target.
            currentIndex = (currentIndex + 1) % pathPoints.Count;
        }
    }

    private void LookAt(Transform target) {
        Quaternion rot = Quaternion.LookRotation((target.transform.position - mesh.Last.transform.position));
        mesh.Last.transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 1000);
    }

    private void FixedUpdate() {
        Follow();
    }
}