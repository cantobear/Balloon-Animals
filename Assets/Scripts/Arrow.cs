using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    public float despawnTime = 5f;

    private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
        lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f) {
            Vector3 direction = gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        }
        lastPosition = transform.position;
    }

    void OnCollisionEnter2D (Collision2D c) {
        if (c.collider.CompareTag("Wall")) {
            RaycastHit2D[] hit = Physics2D.LinecastAll(lastPosition - transform.up * GetComponent<BoxCollider2D>().bounds.extents.y, lastPosition + transform.up * 3f * GetComponent<BoxCollider2D>().bounds.extents.y);
            bool didHit = false;
            foreach (RaycastHit2D x in hit) {
                if (x.collider.CompareTag(c.collider.tag)) {
                    GetComponent<Rigidbody2D>().isKinematic = true;
                    didHit = true;
                    transform.position = lastPosition;
                    transform.position += new Vector3(x.point.x, x.point.y) - transform.position;
                    break;
                }
            }
            if (didHit) {
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<BoxCollider2D>().isTrigger = true;
                StartCoroutine("deleteAfterDelay", despawnTime);
            }
        }
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
