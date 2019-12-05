using System;
using UnityEngine;
using Object = System.Object;

public class MeshCollision : MonoBehaviour {
    [SerializeField] private EnemyBot enemy;
    [SerializeField] private PlayerInput player;

    private void OnEnable() {
        enemy = GetComponentInParent<EnemyBot>();
        if (enemy == null) {
            player = GetComponentInParent<PlayerInput>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (enemy != null) {
            if (!enemy.IsAlive) return;
            Debug.Log("Enemy Collided with" + other.transform.parent.gameObject.name);
            enemy.IsAlive = false;
        }
        else if (player != null) {
            if (!player.IsAlive) return;
            Debug.Log("Player Collided with" + other.transform.parent.gameObject.name);
            player.IsAlive = false;
        }
    }
}