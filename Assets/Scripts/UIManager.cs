using LineAR;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [SerializeField] private GameObject endGamePanel;

    public void ToggleEndGame(bool state) {
        endGamePanel.SetActive(state);
    }

    public void SetEndMessage(string mess, string score) {
        endGamePanel.GetComponent<EndGameScript>().SetMessage(mess,score);
    }
}
