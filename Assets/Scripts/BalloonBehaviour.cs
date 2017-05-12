using UnityEngine;
using System.Collections;

public class BalloonBehaviour : MonoBehaviour {

    public Sprite sprite;
    public float gravity;
    public float maxVelocity;
    public float deceleration;
    private Rigidbody rigidbody;
    private Vector3 windVector;
    public GameObject PoppedBalloonParticles;
    public int balloonValue;
    public static GameStateManager gameStateManager;

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
            rigidbody.velocity -= rigidbody.velocity * Mathf.Min(Mathf.Pow((magnitude - maxVelocity)/2, 1.4f) * (deceleration/magnitude) * Time.fixedDeltaTime, 1);//((magnitude - Mathf.Pow(magnitude/2, 2) * Time.fixedDeltaTime) / magnitude);

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
        pop();
    }

    public virtual void pop() {
        gameStateManager.poppedBalloon(balloonValue);
        Destroy(Instantiate<GameObject>(PoppedBalloonParticles, transform.position, transform.rotation, transform.parent), 0.5f);
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
        gameStateManager.lostBalloon(balloonValue);
        Destroy(gameObject);
    }
}
