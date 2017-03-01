using UnityEngine;
using System.Collections;

public class BalloonBehaviour : MonoBehaviour {

    public Sprite sprite;
    public float gravity;
    public float maxVelocity;
    public float deceleration;

    // Use this for initialization
    void Start() {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Update is called once per frame
    void FixedUpdate() {
        GetComponent<Rigidbody>().AddForce(new Vector3(0,gravity,0), ForceMode.Acceleration);
        float magnitude = GetComponent<Rigidbody>().velocity.magnitude;
        if (magnitude > maxVelocity)
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * ((magnitude - Mathf.Pow(magnitude/2, 2) * Time.fixedDeltaTime) / magnitude);
        float angMagnitude = GetComponent<Rigidbody>().angularVelocity.magnitude;
        //if (angMagnitude > 1)
        //    GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().angularVelocity / ((angMagnitude - 10 * Time.fixedDeltaTime) / angMagnitude);
    }

    void OnCollisionEnter(Collision coll) {
        if (coll.collider.name == "Ground")
            onGrounded();
    }

    public virtual void onHit() {
        Destroy(gameObject);
    }

    public virtual void onPunch() {
        GetComponent<Rigidbody>().AddForce(Vector2.up * 2);
    }

    public virtual void onGrounded() {
        Destroy(gameObject);
    }
}
