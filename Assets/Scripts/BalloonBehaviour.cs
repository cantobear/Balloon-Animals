using UnityEngine;
using System.Collections;

public class BalloonBehaviour : MonoBehaviour {

    public Sprite sprite;
    public float gravity;

    // Use this for initialization
    void Start() {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Update is called once per frame
    void Update() {
        GetComponent<Rigidbody>().AddForce(new Vector3(0,gravity,0), ForceMode.Acceleration);
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
