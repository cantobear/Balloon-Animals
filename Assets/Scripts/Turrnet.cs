﻿using UnityEngine;
using System.Collections;

public class Turrnet : MonoBehaviour {

    GameObject arrowPrefab;
    float charge = 0;

	// Use this for initialization
	void Start () {
        arrowPrefab = Resources.Load<GameObject>("Arrow");
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, gameObject.transform.position.z) - transform.position).normalized;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        if (Input.GetAxis("Fire1") == 1) {
            charge = Mathf.Min(charge + Time.deltaTime, 0.5f);
        }
        else if (charge > 0) {
            fireDirection(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90, charge * 50);
            charge = 0;
        }
        drawTragectory(transform.position, transform.up * charge * 50);
    }

    void drawTragectory(Vector3 startPos, Vector3 velocity) {
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetVertexCount(50);
        Vector2 curPos = startPos;
        Vector2 vel = velocity;

        for(int i = 0; i < 100; i++) {
            if (i%2 == 0)
                line.SetPosition(i/2, curPos);
            vel += Physics2D.gravity * 3 * Time.fixedDeltaTime;
            curPos += vel * Time.fixedDeltaTime;

        }
    }

    void fireDirection(float angle, float speed) {
        GameObject arrow = Instantiate<GameObject>(arrowPrefab);
        arrow.transform.position = transform.position;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        arrow.GetComponent<Rigidbody2D>().velocity = arrow.transform.up * speed;

    }
}