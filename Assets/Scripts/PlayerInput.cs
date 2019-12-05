using System;
using System.Collections;
using System.Collections.Generic;
using LineAR;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Input manager class
/// </summary>
public class PlayerInput : MonoBehaviour {
    // Should have an instance of the player.
    [SerializeField] private MeshGenerator player;
    [SerializeField] private bool isAlive = true;

    private float horizontal;

    public float Horizontal
    {
        get => horizontal;
        set => horizontal = value;
    }

    public bool IsAlive
    {
        get => isAlive;
        set {
            if (value == false) {
                player.StartGrow = false;
            }
        }
    }

    private void Start() {
        player = GetComponent<MeshGenerator>();
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            player.StartGrow = !player.StartGrow;
        }

        // Platform specific input.
#if UNITY_EDITOR_OSX || UNITY_EDITOR || UNITY_EDITOR_64
        horizontal = Input.GetAxis("Horizontal");
#endif

        // control input for mobile.
#if UNITY_ANDROID 
        if (!player.StartGrow) return;
        if (Input.touchCount > 0) {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Stationary || t.phase != TouchPhase.Ended) {
                if (t.position.x < Screen.width / 2) {
                    horizontal = -1;
                }
                else if (t.position.x > Screen.width / 2) {
                    horizontal = 1;
                }
            }
            else {
                horizontal = 0;
            }
        }
#endif
    }
}