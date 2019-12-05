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
        if (other.gameObject.name == "Ground Plane Stage" || other.gameObject.name == "Ground") return;
        
        if (enemy != null) {
            if (!enemy.IsAlive) {
                return;
            }
            
            enemy.IsAlive = false;

            Debug.Log("Enemy Collided with " + other.transform.parent.gameObject.name);
            Debug.Log("Enemy Collided with " + other.gameObject.name);
        }
        else if (player != null) {
            if (!player.IsAlive) return;
            Debug.Log("Player Collided with " + other.transform.parent.gameObject.name);
            Debug.Log("Player Collided with " + other.gameObject.name);
            player.IsAlive = false;
            
            GameManager.Instance.DefeatedGame();
        }
    }
}