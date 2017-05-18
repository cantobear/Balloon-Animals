using UnityEngine;
using System.Collections;

public class BalloonBehaviour : MonoBehaviour {

    public Sprite sprite;
    //public float gravity;
    public float maxVelocity;
    public float deceleration;
    private Rigidbody2D rigidbody;
    private Vector2 windVector;
    public GameObject PoppedBalloonParticles;
    public int balloonValue;
    public static GameStateManager gameStateManager;
    private int seed;
    private float heat;
    public float maxHeat;
    public float heatCoolDownPerSec;

    void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start() {
        GetComponent<SpriteRenderer>().sprite = sprite;
        seed = Random.Range(0, 1000);
    }

    // Update is called once per frame
    void FixedUpdate() {
        //rigidbody.AddForce(new Vector3(0,gravity,0), ForceMode.Acceleration);
        rigidbody.velocity += windVector;
        float magnitude = rigidbody.velocity.magnitude;
        if (magnitude > maxVelocity)
            rigidbody.velocity -= rigidbody.velocity * Mathf.Min(Mathf.Pow((magnitude - maxVelocity)/2, 1.4f) * (deceleration/magnitude) * Time.fixedDeltaTime, 1);//((magnitude - Mathf.Pow(magnitude/2, 2) * Time.fixedDeltaTime) / magnitude);

        if (heat > maxHeat) {
            onMaxHeat();
        }
        removeHeat(heatCoolDownPerSec * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.name == "Ground")
            onGrounded();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Wind")) {
            addWind(coll.gameObject.GetComponent<Wind>().getRandomWindVector(seed));
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.CompareTag("Wind")) {
            removeWind(coll.gameObject.GetComponent<Wind>().getRandomWindVector(seed));
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

    public virtual void addWind(Vector2 v) {
        windVector += v;
    }

    public virtual void removeWind(Vector2 v) {
        windVector -= v;
    }

    protected virtual void onMaxHeat() {
        pop();
    }

    public virtual void addHeat(float amount) {
        heat += amount;
        updateHeatColor();
    }

    public virtual void removeHeat(float amount) {
        heat = Mathf.Max(0, heat - amount);
        updateHeatColor();
    }

    private void updateHeatColor() {
        float colorValue = 1 - (heat / maxHeat) / 1.33f;
        Color burnColor = new Color(Mathf.Sqrt(colorValue), colorValue, colorValue);
        GetComponent<SpriteRenderer>().color = burnColor;
    }

    public virtual void onGrounded() {
        gameStateManager.lostBalloon(balloonValue);
        Destroy(gameObject);
    }
}
