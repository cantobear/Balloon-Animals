using UnityEngine;
using System.Collections;

public class ddrGameManager : MonoBehaviour {

    int multiplier = 1;
    int streak = 0;

	void Start () {
        PlayerPrefs.SetInt("Score", 0);
	}
	

	void Update () {
	
	}

    public int GetScore()
    {
        return 2 * multiplier;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }

}
