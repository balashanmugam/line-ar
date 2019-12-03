using System;
using System.Collections;
using System.Collections.Generic;
using LineAR;
using UnityEngine;
/// <summary>
/// Input manager class
/// </summary>
public class PlayerInput : MonoBehaviour
{
    // Should have an instance of the player.
    [SerializeField] private MeshGenerator player;

    private float horizontal;

    public float Horizontal
    {
        get => horizontal;
        set => horizontal = value;
    }

    private void Start() {
        player = GetComponent<MeshGenerator>();
    }
    
    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            Debug.Log("Space pressed. ");
            player.StartGrow = !player.StartGrow;
        }
        
        // control input.
        horizontal = Input.GetAxis("Horizontal");


    }
}
