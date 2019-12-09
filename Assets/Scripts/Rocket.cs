using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Rocket : MonoBehaviour {
    [SerializeField] private float velocity = 1;
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,velocity);
    }

    private void FixedUpdate() {
        
    }
}
