using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    public float despawnTime = 5f;
    private Rigidbody2D rigidbody;
    //public float gravity;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rigidbody.velocity.magnitude > 0.1f && rigidbody.bodyType == RigidbodyType2D.Dynamic) {
            Vector3 direction = rigidbody.velocity.normalized;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        }
        //GetComponent<Rigidbody2D>().AddForce(new Vector3(0, gravity, 0), ForceMode.Acceleration);
    }

    void OnCollisionEnter2D (Collision2D c) {
        if (c.collider.CompareTag("Wall") || c.collider.CompareTag("Player")) {
            //Debug.DrawRay(transform.position - transform.up * GetComponent<BoxCollider2D>().bounds.extents.y, transform.up * 3f, Color.blue, 3);
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position - transform.up * GetComponent<BoxCollider2D>().bounds.extents.y, transform.up, 2f);
            bool didHit = false;
            foreach (RaycastHit2D x in hit) {
                if (x.collider.CompareTag(c.collider.tag)) {
                    didHit = true;
                    transform.position = new Vector3(x.point.x, x.point.y);
                    Debug.DrawRay(new Vector3(x.point.x, x.point.y), transform.position - new Vector3(x.point.x, x.point.y), Color.red, 3);
                    break;
                }
            }
            if (didHit) {
                if (c.collider.CompareTag("Player"))
                    transform.parent = c.transform;
                rigidbody.bodyType = RigidbodyType2D.Kinematic;
                GetComponent<BoxCollider2D>().enabled = false;
                rigidbody.velocity = Vector3.zero;
                StartCoroutine("deleteAfterDelay", despawnTime);
            }
            if (c.collider.name == "Ground") {
                gameObject.layer = 18;
                StartCoroutine("deleteAfterDelay", despawnTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.CompareTag("Balloon"))
            c.transform.parent.GetComponent<BalloonBehaviour>().onHit();
    }

    IEnumerator deleteAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        Color c = Color.black;
        c.a = GetComponent<SpriteRenderer>().color.a / 10;
        do {
            GetComponent<SpriteRenderer>().color -= c;
            yield return new WaitForSeconds(0.1f);
        } while (GetComponent<SpriteRenderer>().color.a > 0);
        Destroy(gameObject);
    }
}
