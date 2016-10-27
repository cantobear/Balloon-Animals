using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ground : MonoBehaviour {

    private int count;
    public GUIText scoreText;

	// Use this for initialization
	void Start ()
    {
        count = 0;
        UpdateCount();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Balloon"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            UpdateCount();
        }
    }
    void UpdateCount()
    {
        scoreText.text = "Counter: " + count;
    }
}
