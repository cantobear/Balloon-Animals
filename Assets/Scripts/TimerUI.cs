using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {

    private GameStateManager gameStateManager;
    private Text text;

    void Awake() {
        gameStateManager = GameObject.FindObjectOfType<GameStateManager>();
        text = GetComponent<Text>();
    }

    void Update() {
        string seconds = ((int)Mathf.Ceil(gameStateManager.timeLimit - gameStateManager.time) % 60).ToString();
        text.text = ((int)Mathf.Ceil(gameStateManager.timeLimit - gameStateManager.time) / 60).ToString() + ":" + (seconds.Length == 1 ? "0" + seconds : seconds);
    }
}
