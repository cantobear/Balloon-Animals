using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	void Start () {
        rb.velocity = new Vector2(0, -speed);
	}
	
	void Update () {
	    
	}
}
