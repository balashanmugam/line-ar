using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    [SerializeField] private float velocity = 0.6f;

    private Rigidbody rb;
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private bool hasExploded = false;

    [SerializeField] private List<GameObject> collidedObjects;

    [SerializeField] private GameObject pointOfExplosion;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * velocity;
    }

    private void OnTriggerEnter(Collider other) {
        if (!hasExploded) {
            if (other.CompareTag("character")) {
                // create an explosion and create sphere collider and destroy only the meshes in it.
                Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);

                Collider[] hitColliders = Physics.OverlapSphere(pointOfExplosion.transform.position, 0.1f);
                int i = 0;
                while (i < hitColliders.Length) {
                    if (hitColliders[i].transform.CompareTag("character")) {
                        hitColliders[i].gameObject.SetActive(false);
                    }

                    i++;
                }

                gameObject.SetActive(false);
            }
        }
    }
}