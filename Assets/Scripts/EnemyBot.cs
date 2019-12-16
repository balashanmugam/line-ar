using System;
using System.Collections.Generic;
using LineAR;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
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

    //making enemies shoot bombs
    [SerializeField] private GameObject rocket;
    [SerializeField] private bool isReadyForBomb;

    [SerializeField] private float bombTicker;
    [SerializeField] private float bombTimer = 1.5f;

    public bool IsAlive
    {
        get => isAlive;
        set {
            isAlive = value;
            if (value == false) {
                mesh.StartGrow = false;
                GameManager.Instance.EnemyLostCount++;
            }
        }
    }

    // launch bomb when the the player is close to the curves.
    private void LaunchBomb() {
        // Instantiate bomb
        if (!isReadyForBomb) return;
        var bomb = Instantiate(rocket, mesh.RbCircle.transform.position + (mesh.RbCircle.transform.forward * 0.05f),
            Quaternion.Euler(mesh.RbCircle.transform.rotation.eulerAngles));
        isReadyForBomb = false;
        Debug.Log("Bomb LAunched!");
    }

    public Transform PathParent
    {
        get => pathParent;
        set {
            pathParent = value;
            int pathRandomIndex = Random.Range(0, pathParent.childCount);
            for (int i = 0; i < pathParent.transform.GetChild(pathRandomIndex).gameObject.transform.childCount; i++) {
                pathPoints.Add(pathParent.transform.GetChild(pathRandomIndex).gameObject.transform.GetChild(i));
            }
        }
    }

    private void Start() {
        mesh = GetComponent<MeshGenerator>();
    }

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

    private void Update() {
        RaycastHit hit;
        //Debug.DrawRay(mesh.RbCircle.transform.position + (mesh.RbCircle.transform.forward * 0.05f), mesh.RbCircle.transform.forward * 0.5f, Color.cyan);

        bombTicker += Time.deltaTime;
        if (!(bombTicker >= bombTimer)) return;
        isReadyForBomb = true;
        bombTicker = 0;


        if (!isAlive) return;
        if (!isReadyForBomb) return;
        // raycast
        if (Physics.Raycast(mesh.RbCircle.transform.position+(mesh.RbCircle.transform.forward * 0.05f), mesh.RbCircle.transform.forward * 0.5f, out hit)) {
//            Debug.Log("Hitting with " + hit.collider.gameObject.name);
//            Debug.DrawLine(mesh.RbCircle.transform.position,hit.collider.transform.position,Color.magenta,2);

            if (hit.collider.CompareTag("character")) {
                //Debug.DrawRay(mesh.RbCircle.transform.position, mesh.RbCircle.transform.forward * 0.5f, Color.cyan);
                LaunchBomb();
            }
        }
    }


    private void FixedUpdate() {
        Follow();
    }
}