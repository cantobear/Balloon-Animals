using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    public float despawnTime = 5f;
    public float gravity;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GetComponent<Rigidbody>().velocity.magnitude > 0.1f) {
            Vector3 direction = gameObject.GetComponent<Rigidbody>().velocity.normalized;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        }
        GetComponent<Rigidbody>().AddForce(new Vector3(0, gravity, 0), ForceMode.Acceleration);
    }

    void OnCollisionEnter (Collision c) {
        if (c.collider.CompareTag("Wall") || c.collider.CompareTag("Player")) {
            RaycastHit[] hit = Physics.RaycastAll(transform.position - transform.up * GetComponent<BoxCollider>().bounds.extents.y, transform.position + transform.up * 3f * GetComponent<BoxCollider>().bounds.extents.y);
            bool didHit = false;
            foreach (RaycastHit x in hit) {
                if (x.collider.CompareTag(c.collider.tag)) {
                    GetComponent<Rigidbody>().isKinematic = true;
                    didHit = true;
                    transform.position += new Vector3(x.point.x, x.point.y) - transform.position;
                    break;
                }
            }
            if (didHit) {
                if (c.collider.CompareTag("Player"))
                    transform.parent = c.transform;
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<BoxCollider>().enabled = false;
                StartCoroutine("deleteAfterDelay", despawnTime);
            }
            if (c.collider.name == "Ground") {
                gameObject.layer = 18;
                StartCoroutine("deleteAfterDelay", despawnTime);
            }
        }
    }

    void OnTriggerEnter(Collider c) {
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
