using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;


namespace LineAR {
    public class MeshGenerator : MonoBehaviour {

        
        [SerializeField] private bool startGrow = false;
        [SerializeField] private GameObject unitCircle;

        [SerializeField] private GameObject last;

        [SerializeField] private float TIMESTEP = 0.01f;
        [SerializeField] private float timer = 0.5f;

        [SerializeField] private float horizontal;

        [SerializeField] private PlayerInput input;

        public bool StartGrow
        {
            get => startGrow;
            set => startGrow = value;
        }

        private void Start() {
            last = this.gameObject;
            input = GetComponent<PlayerInput>();
            Spawn();
        }
        private void Spawn() {
            //spawn a small cylinder
            var obj = Instantiate(unitCircle, last.transform.position + (transform.forward * 0.05f), Quaternion.identity, gameObject.transform);
            if (obj != null) {
                last = obj;
            }
        }

        private void Update() {
            // get input from the PlayerInput class
            horizontal = input.Horizontal;
            Debug.Log(horizontal);
        }

        private void FixedUpdate() {
            // keep spawning
            if (!startGrow) return;
            
            timer -= Time.fixedDeltaTime;
            if (timer <= 0) {
                Spawn();
                timer = TIMESTEP;
            }
            // rotate according to user input
            if (horizontal < 0 ) {
                // Forward to left
            } else if (horizontal > 0) {
                // Forward to right
            }
        }
    }
}