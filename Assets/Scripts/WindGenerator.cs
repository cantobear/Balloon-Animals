using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGenerator : MonoBehaviour {

    public GameObject Wind;
    private Collider2D collider;
	
    void Awake() {
        collider = GetComponent<Collider2D>();
    }

	// Update is called once per frame
	void FixedUpdate () {
		if (Random.Range(0, 5) == 0) {
            GameObject wind = Instantiate<GameObject>(Wind);
            wind.transform.parent = transform;
            wind.transform.position = new Vector2(Random.Range(-collider.bounds.extents.x, collider.bounds.extents.x), Random.Range(-collider.bounds.extents.y, collider.bounds.extents.y));
            wind.transform.localScale = new Vector2(Random.Range(2f, 5f), Random.Range(1f, 2f));
            wind.GetComponent<Wind>().windVector = new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.05f, 0.1f));
            wind.GetComponent<Wind>().speedRange = new Vector2(Random.Range(0, 0.1f), Random.Range(-0.025f, 0.05f));
            wind.GetComponent<Wind>().destroy(Random.Range(1f, 5f));
        }
	}
}
