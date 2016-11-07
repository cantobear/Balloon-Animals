using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.5f) {
            Vector3 direction = gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        }
    }

    void OnCollisionEnter2D (Collision2D c) {
        if (c.collider.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
