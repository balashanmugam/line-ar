using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;


namespace LineAR {
    public class MeshGenerator : MonoBehaviour {
        [SerializeField] private GameObject unitCircle;

        [SerializeField] private GameObject last;

        [SerializeField] private float TIMESTEP = 0.01f;
        [SerializeField] private float timer = 0.01f;

        private void Start() {
            last = this.gameObject;
            Spawn();

        }

        private void Spawn() {
            //spawn a small cylinder
            var obj = Instantiate(unitCircle, last.transform.position + (transform.forward * 0.05f), Quaternion.identity, gameObject.transform);
            if (obj != null) {
                last = obj;
            }
        }

        private void FixedUpdate() {
            // keep spawning
            timer -= Time.fixedDeltaTime;
            if (timer <= 0) {
                Spawn();
                timer = TIMESTEP;
            }
        }
    }
}