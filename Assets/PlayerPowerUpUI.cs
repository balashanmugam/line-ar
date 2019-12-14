using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerUpUI : MonoBehaviour {
    [SerializeField] private Button powerUpButton1;

    public delegate void PowerUpButton();

    public static event PowerUpButton OnPowerUpPressed;

    void Awake() {
        powerUpButton1 = GetComponent<Button>();
        powerUpButton1.onClick.AddListener(PowerUpCalled);
    }

    void PowerUpCalled() {
        Debug.Log("LMAO");
        if (OnPowerUpPressed != null) {
            OnPowerUpPressed();
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Hit with " + other.gameObject.name);
    }
}