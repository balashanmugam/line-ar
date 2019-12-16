using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ObjectPoolSystem : Singleton<ObjectPoolSystem> {
    [SerializeField] private GameObject player, enemy;
    [SerializeField] public static Queue<GameObject> playerCylinders = new Queue<GameObject>();
    [SerializeField] public static Queue<GameObject> enemyCylinders = new Queue<GameObject>();

    private void Awake() {
        // fill in with thousand objects
        for (int i = 0; i < 2000; i++) {
            var obj = Instantiate(player, Vector3.zero, Quaternion.identity, this.gameObject.transform);
            obj.SetActive(false);
            playerCylinders.Enqueue(obj);
        }

        for (int i = 0; i < 5000; i++) {
            var obj = Instantiate(enemy, Vector3.zero, Quaternion.identity, this.gameObject.transform);
            obj.SetActive(false);
            enemyCylinders.Enqueue(obj);
        }
    }

    public static GameObject Instantiate(int id, Vector3 pos, Quaternion rot, Transform parent) {
        GameObject obj = id == 0 ? playerCylinders.Dequeue() : enemyCylinders.Dequeue();
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.transform.SetParent(parent);
        obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        obj.SetActive(true);
        return obj;
    }

    public static void Destroy(GameObject obj, int spawnId) {
        obj.SetActive(false);
        if (spawnId == 0) playerCylinders.Enqueue(obj);
        else enemyCylinders.Enqueue(obj);
    }

    private void Update() {
        if (playerCylinders.Count >= 100) return;
        // add 2000 more
        for (int i = 0; i < 2000; i++) {
            var obj = Instantiate(player, Vector3.zero, Quaternion.identity, this.gameObject.transform);
            obj.SetActive(false);
            playerCylinders.Enqueue(obj);
        }

        if (enemyCylinders.Count >= 200) return;
        for (int i = 0; i < 3000; i++) {
            var obj = Instantiate(enemy, Vector3.zero, Quaternion.identity, this.gameObject.transform);
            obj.SetActive(false);
            enemyCylinders.Enqueue(obj);
        }
    }
}