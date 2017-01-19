﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{

    Animator anim;
    public string horizontalControl = "Horizontal";
    public string jumpControl = "Jump";

	private Rigidbody2D rb;
	private bool grounded;
	private Collider2D coll;

	public float maxHorizontalSpeed = 7f;
	public float horizontalAccel = 50f;
	public float jumpAccel = 300f;
    public float deceleration = 0.5f;

    // Use this for initialization
    void Start ()
    {
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update()
    {
		if (coll.IsTouchingLayers(LayerMask.GetMask ("Ground")))
        {
			grounded = true;
            anim.SetBool("Jump", false); //stops jump animation
        }
	}
	void FixedUpdate ()
    {
        if (grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x - Mathf.Sign(rb.velocity.x) * Mathf.Min(Mathf.Abs(rb.velocity.x), deceleration) , rb.velocity.y);
            if (Input.GetAxis(jumpControl) > 0)
            {
                rb.AddForce(transform.up * jumpAccel);
                grounded = false;
                anim.SetBool("Jump", true); //triggers jump animation
            }
        }
        rb.AddForce(transform.right * Input.GetAxis(horizontalControl) * horizontalAccel * (grounded ? 1 : 0.25f));
        if (Input.GetAxis(horizontalControl) != 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(Input.GetAxis(horizontalControl)), transform.localScale.y, transform.localScale.z);
            anim.SetInteger("Speed", 1); // for walking animation
        }
        else
            anim.SetInteger("Speed", 0); // stops walking animation
        
        //Caps the player's horizontal velocity
        Vector2 velocity = rb.velocity;
		velocity.x = Mathf.Clamp (velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
		rb.velocity = velocity;
    }
}
