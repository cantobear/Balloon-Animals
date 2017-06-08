using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {
    
    private Text text;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        int time = (int)Mathf.Ceil(Mathf.Max(GameStateManager.timeRemaining, 0));
        string seconds = (time % 60).ToString();
        text.text = (time / 60).ToString() + ":" + (seconds.Length == 1 ? "0" + seconds : seconds);
    }
}
