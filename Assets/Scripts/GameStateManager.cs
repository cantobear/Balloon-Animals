using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {

    public int balloonsLostLimit;
    private int _balloonsLost;
    public int balloonsLost {
        get { return _balloonsLost; }
    }
    private int _balloonsPopped;
    public int balloonsPopped {
        get { return _balloonsPopped; }
    }
    public float timeLimit;
    private float startTime;
    public float time {
        get { return Time.time - startTime; }
    }

    public GameObject button;

    private enum GameState {
        start, progressing, gameOver
    }

    void Awake() {
        BalloonBehaviour.gameStateManager = this;
    }

    void Start() {
        startTime = Time.time;
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

    void Update() {
        switch(checkGameState()) {
            case GameState.start:
                break;
            case GameState.progressing:
                break;
            case GameState.gameOver:
                endGame(balloonsLost > balloonsLostLimit);
                break;
            default:
                break;
        }
    }

    private GameState checkGameState() {
        if (time > timeLimit || balloonsLost > balloonsLostLimit)
            return GameState.gameOver;
        else if (time < 3)
            return GameState.start;
        else
            return GameState.progressing;
    }

    private void endGame(bool winner) {
        Debug.Log(winner ? "Dropper Wins!" : "Pusher Wins!");
        button.SetActive(true);
    }

    public void restartGame() {
        SceneManager.LoadScene("Brian");
    }
}
