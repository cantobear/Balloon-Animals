using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonsLostText : MonoBehaviour {
    
    private Text text;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = "Balloons Lost: " + GameStateManager.balloonsLost.ToString();
    }
}
