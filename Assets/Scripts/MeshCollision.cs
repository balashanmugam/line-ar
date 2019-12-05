using System;
using UnityEngine;
using Object = System.Object;

public class MeshCollision : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
    }
}
