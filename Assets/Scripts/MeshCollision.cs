using System;
using UnityEngine;
using Object = System.Object;

public class MeshCollision : MonoBehaviour {

    //[SerializeField] private GameObject rbCircle;

    private void OnEnable() {

        //rbCircle = this.transform.Find("RigidbodyCircle(Clone)").transform.Find("Cylinder").gameObject;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
    }
}
