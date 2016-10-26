﻿using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {

	private Rigidbody2D rb;
	private bool grounded;
	private Collider2D coll;

	public float jumpAccel = 500f;

	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody2D> ();
		coll = GetComponent<Collider2D>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (coll.IsTouchingLayers(LayerMask.GetMask ("Ground"))) {
			grounded = true;
		}
	}

	void FixedUpdate () {
	
		if ((Input.GetKey ("up") || Input.GetKey (KeyCode.W)) && grounded) {
			rb.AddForce (transform.up * jumpAccel);
			grounded = false;
		}
	}
}
