using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;

	public float maxHorizontalSpeed = 5f;
	public float horizontalAccel = 50f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update(){
	}
	void FixedUpdate () {

		if (Input.GetKey("right") || Input.GetKey (KeyCode.D)) {
			rb.AddForce (transform.right * horizontalAccel);
		}
		if (Input.GetKey ("left") || Input.GetKey (KeyCode.A)) {
			rb.AddForce (transform.right * -horizontalAccel);
		}

		//Caps the player's horizontal velocity
		Vector2 velocity = rb.velocity;
		velocity.x = Mathf.Clamp (velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
		rb.velocity = velocity;
	}
}
