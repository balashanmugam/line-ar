using LineAR;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private Button startButton;

    [SerializeField] private MeshGenerator player;

    private void OnEnable() {
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame() {
        player.StartGrow = !player.StartGrow;
    }
    
    private void OnDisable() {
        startButton.onClick.RemoveListener(StartGame);
    }
    
    
}
