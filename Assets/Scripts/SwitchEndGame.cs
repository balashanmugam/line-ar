using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEndGame : MonoBehaviour
{
    public GameObject endgame;
    public void EnableEndGame()
    {
        endgame.SetActive(true);
    }
}
