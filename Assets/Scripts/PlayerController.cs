using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private bool grounded;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update(){
		if (!grounded && rb.velocity.y == 0) {
			grounded = true;
		}
	}
	void FixedUpdate () {
	
		if ((Input.GetKey ("up") || Input.GetKey (KeyCode.W)) && grounded) {
			rb.AddForce (transform.up * 100);
			grounded = false;
		}
		if (Input.GetKey("right") || Input.GetKey (KeyCode.D)) {
			rb.AddForce (transform.right * 10);
		}
		if (Input.GetKey ("left") || Input.GetKey (KeyCode.A)) {
			rb.AddForce (transform.right * -10);
		}
	}
}
