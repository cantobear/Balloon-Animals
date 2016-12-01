using UnityEngine;
using System.Collections;

public class BalloonBehaviour : MonoBehaviour {

    public Sprite sprite;

    // Use this for initialization
    void Start() {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.name == "Ground")
            onGrounded();
    }

    public virtual void onHit() {
        Destroy(gameObject);
    }

    public virtual void onPunch() {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2);
    }

    public virtual void onGrounded() {
        Destroy(gameObject);
    }
}
