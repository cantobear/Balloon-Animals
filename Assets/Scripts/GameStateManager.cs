using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    private int _balloonsLost;
    public int balloonsLost {
        get { return _balloonsLost; }
    }
    private int _balloonsPopped;
    public int balloonsPopped {
        get { return _balloonsPopped; }
    }

    void Awake() {
        BalloonBehaviour.gameStateManager = this;
    }

    public void poppedBalloon(int value) {
        _balloonsPopped += value;
    }

    public void lostBalloon(int value) {
        _balloonsLost += value;
    }

    public void resetGame() {
        resetBalloonsLost();
    }

    private void resetBalloonsLost() {
        _balloonsLost = 0;
    }

}
