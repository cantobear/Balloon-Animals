﻿using UnityEngine;
using System.Collections;

public class DropBalloon : MonoBehaviour {

    public float balloonDelay = 0.1f;
    public string dropControl = "Drop";
    public string dropAllControl = "Drop All";
    public int dropAllAmmount = 10;
    public int balloonCount = 100;
    private float zPosition = 0;
    bool dropped = false;
    public GameObject[] balloons;

    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("bCount", balloonCount); //sets the text for the total number of starting balloons
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis(dropControl) == 0 && Input.GetAxis(dropAllControl) == 0)
            dropped = false;
        else if (!dropped)
        {
            dropped = true;
            if (balloonCount > 0)
            {
                if (Input.GetAxis(dropControl) != 0)
                    dropBalloon();
                else if (Input.GetAxis(dropAllControl) != 0)
                    dropBalloons(dropAllAmmount);
            }
        }
        balloonCount = PlayerPrefs.GetInt("bCount"); //Sets the balloonCount to the number displayed
    }

    GameObject dropBalloon() {
        --balloonCount;
        GameObject spawned = Instantiate<GameObject>(balloons[Random.Range(0, 3)]);
        zPosition += 0.000001f; //Makes sure that monsters always spawn on diffrent layers so there is no z-fighting
        spawned.transform.position = transform.position + Vector3.forward * zPosition;
        //spawned.transform.Translate(Vector3.down * 6.8f);
        spawned.transform.Rotate(new Vector3(0, 0, Random.Range(1, 360)));
        spawned.transform.SetParent(GameStateManager.go.transform);

        PlayerPrefs.SetInt("bCount", balloonCount); //Subtracts a balloon on the display
        return spawned;
    }

    void dropBalloons(int count) {
        dropBalloonsInstant(Mathf.Min(count, balloonCount));
    }

    void dropBalloonsInstant(int count) {
        for (int i = count; i > 0; --i) {
            GameObject spawned = dropBalloon();
            spawned.transform.position += new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            spawned.GetComponent<Rigidbody2D>().velocity += Vector2.right * Random.Range(-2f, 2f) + Vector2.up * Random.Range(3f, 4f);
        }
    }

    IEnumerator dropBalloonsCoroutine(int count) {
        for (int i = count; i > 0; --i) {
            GameObject spawned = dropBalloon();
            spawned.transform.position += new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            spawned.GetComponent<Rigidbody2D>().velocity += Vector2.right * Random.Range(-2f, 2f) + Vector2.up * Random.Range(3f, 4f);
            yield return new WaitForSeconds(balloonDelay/count);
        }
    }
}
