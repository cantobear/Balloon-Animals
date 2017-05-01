﻿using UnityEngine;
using System.Collections;

public class BalloonBehaviour : MonoBehaviour {

    public Sprite sprite;
    public float gravity;
    public float maxVelocity;
    public float deceleration;
    private Rigidbody rigidbody;
    public Vector3 windVector;

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start() {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Update is called once per frame
    void FixedUpdate() {
        rigidbody.AddForce(new Vector3(0,gravity,0), ForceMode.Acceleration);
        rigidbody.velocity += windVector;
        float magnitude = rigidbody.velocity.magnitude;
        if (magnitude > maxVelocity)
            rigidbody.velocity = rigidbody.velocity * ((magnitude - Mathf.Pow(magnitude/2, 2) * Time.fixedDeltaTime) / magnitude);

        }

    void OnCollisionEnter(Collision coll) {
        if (coll.collider.name == "Ground")
            onGrounded();
    }

    void OnTriggerEnter(Collider coll) {
        if (coll.CompareTag("Wind")) {
            addWind(coll.gameObject.GetComponent<Wind>().windVector);
        }
    }

    void OnTriggerExit(Collider coll) {
        if (coll.CompareTag("Wind")) {
            removeWind(coll.gameObject.GetComponent<Wind>().windVector);
        }
    }

    public virtual void onHit() {
        Destroy(gameObject);
    }

    public virtual void onPunch(Vector3 fromPos, float velocity) {
        rigidbody.velocity = (transform.position - fromPos) * velocity;
    }

    public virtual void addWind(Vector3 v) {
        windVector += v;
    }

    public virtual void removeWind(Vector3 v) {
        windVector -= v;
    }

    public virtual void onGrounded() {
        Destroy(gameObject);
    }
}
