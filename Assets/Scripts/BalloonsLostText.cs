using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonsLostText : MonoBehaviour {

    private GameStateManager gameStateManager;
    private Text text;

    void Awake() {
        gameStateManager = GameObject.FindObjectOfType<GameStateManager>();
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = "Balloons Lost: " + gameStateManager.balloonsLost.ToString();
    }
}
