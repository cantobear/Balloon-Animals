using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private bool grounded;
	private Collider2D coll;

	public float maxHorizontalSpeed = 5f;
	public float horizontalAccel = 50f;
	public float jumpAccel = 500f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void Update(){
		if (coll.IsTouchingLayers(LayerMask.GetMask ("Ground"))) {
			grounded = true;
		}
	}
	void FixedUpdate () {

		if (Input.GetKey("right") || Input.GetKey (KeyCode.D)) {
			rb.AddForce (transform.right * horizontalAccel);
		}
		if (Input.GetKey ("left") || Input.GetKey (KeyCode.A)) {
			rb.AddForce (transform.right * -horizontalAccel);
		}
		if ((Input.GetKey ("up") || Input.GetKey (KeyCode.W)) && grounded) {
			rb.AddForce (transform.up * jumpAccel);
			grounded = false;
		}

		//Caps the player's horizontal velocity
		Vector2 velocity = rb.velocity;
		velocity.x = Mathf.Clamp (velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
		rb.velocity = velocity;
	}
}
