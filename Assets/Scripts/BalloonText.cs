using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BalloonText : MonoBehaviour {

    public string name1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = PlayerPrefs.GetInt(name1) + "";
    }
}
