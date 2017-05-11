using UnityEngine;
using System.Collections;

public class DropBalloon : MonoBehaviour {

    public float balloonDelay = 0.1f;
    public string dropControl = "Drop";
    public string dropAllControl = "Drop All";
    public int balloonCount = 100;
    private float zPosition = 0;
    bool dropped = false;
    GameObject balloon;
    Sprite[] balloonSprites;

    // Use this for initialization
    void Start () {
        balloon = Resources.Load<GameObject>("Balloon");
        balloonSprites = Resources.LoadAll<Sprite>("Sprites/balloons");
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
                    dropBalloons(10);
            }
        }
        balloonCount = PlayerPrefs.GetInt("bCount"); //Sets the balloonCount to the number displayed
    }

    GameObject dropBalloon() {
        --balloonCount;
        GameObject spawned = Instantiate<GameObject>(balloon);
        zPosition += 0.000001f; //Makes sure that monsters always spawn on diffrent layers so there is no z-fighting
        spawned.transform.position = transform.position + Vector3.forward * zPosition;
        //spawned.transform.Translate(Vector3.down * 6.8f);
        spawned.transform.Rotate(new Vector3(0, 0, Random.Range(1, 360)));

        spawned.GetComponent<BalloonBehaviour>().sprite = balloonSprites[Random.Range(0, 3)];
        PlayerPrefs.SetInt("bCount", balloonCount--); //Subtracts a balloon on the display
        return spawned;
    }

    void dropBalloons(int count) {
        StartCoroutine("dropBalloonsCoroutine", Mathf.Min(count, balloonCount));
    }


    IEnumerator dropBalloonsCoroutine(int count) {
        for (int i = count; i > 0; --i) {
            GameObject spawned = dropBalloon();
            spawned.transform.position += new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            spawned.GetComponent<Rigidbody>().velocity += Vector3.right * Random.Range(-2f, 2f) + Vector3.up * Random.Range(3f, 4f);
            yield return new WaitForSeconds(balloonDelay/count);
        }
    }
}
