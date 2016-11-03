using UnityEngine;
using System.Collections;

public class Turrnet : MonoBehaviour {

    GameObject arrowPrefab;

	// Use this for initialization
	void Start () {
        arrowPrefab = Resources.Load<GameObject>("Arrow");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Fire1") == 1) {
            GameObject arrow = Instantiate<GameObject>(arrowPrefab);
            arrow.transform.position = transform.position;
            Vector3 direction = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, gameObject.transform.position.z) - transform.position).normalized;
            float mouseDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, mouseDirection);
            arrow.GetComponent<Rigidbody2D>().velocity = arrow.transform.up * 18;
        }
	}
}
