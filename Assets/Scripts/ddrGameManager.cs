using UnityEngine;
using System.Collections;

public class ddrGameManager : MonoBehaviour {

    public int PointsPerBalloon;
    int streak = 0;

	void Start () {
        PlayerPrefs.SetInt("Score", 0);
	}
	

	void Update () {
	
	}

    public int GetScore()
    {
        return PointsPerBalloon;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }

}
