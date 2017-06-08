using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {

    public static GameObject go;

    public static int balloonsLostLimit = 100;
    private static int _balloonsLost;
    public static int balloonsLost {
        get { return _balloonsLost; }
    }
    private static int _balloonsPopped;
    public static int balloonsPopped {
        get { return _balloonsPopped; }
    }
    public static float timeLimit = 120;
    private static float startTime;
    public static float time {
        get { return Time.time - startTime; }
    }
    public static float timeRemaining {
        get { return timeLimit - time; }
    }

    public GameObject button;

    public static GameState gameState = GameState.start;
    public enum GameState {
        start, progressing, gameOver
    }


    void Awake() {
        go = gameObject;
    }

    void Start() {
        resetGame();

    }

    public static void poppedBalloon(int value) {
        _balloonsPopped += value;
    }

    public static void lostBalloon(int value) {
        _balloonsLost += value;
    }

    public static void resetGame() {
        resetBalloonsLost();
        resetTime();
    }

    private static void resetBalloonsLost() {
        _balloonsLost = 0;
    }

    private static void resetTime() {
        startTime = Time.time;
    }

    void Update() {
        gameState = checkGameState();
        switch (gameState) {
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
        resetGame();
        SceneManager.LoadScene("Brian");
    }
}
