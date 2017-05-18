using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTurrent : TurrentWeapon {
    
    public GameObject arrowPrefab;

    public float firePower = 50;
    private float charge = 0f;
    public float chargeTime;
    private float chargeTimePassed;
    public float chargeSpeedExponent;

    public int maxArrowCount;
    private float _arrowCount;
    public float arrowCount {
        get { return _arrowCount; }
    }
    public float arrowRespawnTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _arrowCount = Mathf.Min(arrowCount + Time.deltaTime / arrowRespawnTime, maxArrowCount);
        if (charge == 1) {
            drawTragectory(transform.position, transform.up * charge * firePower, Color.yellow);
        }
        else
            drawTragectory(transform.position, transform.up * charge * firePower, Color.white);
    }

    public override bool hasAmmo() {
        return _arrowCount >= 1;
    }

    public override void triggerDown() {
        chargeTimePassed = Mathf.Min(chargeTimePassed + Time.deltaTime / chargeTime, 1);
        charge = 1 - Mathf.Pow((-chargeTimePassed + 1) , chargeSpeedExponent);
    }

    public override void triggerUp() {
        if (charge == 1) {
            fireDirection(Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg - 85, charge * firePower);
            fireDirection(Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg - 90, charge * firePower);
            fireDirection(Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg - 95, charge * firePower);
            charge = chargeTimePassed = 0;
            --_arrowCount;
        }
        else if (charge > 0) {
            fireDirection(Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg - 90, charge * firePower);
            charge = chargeTimePassed = 0;
            --_arrowCount;
        }
    }

    void drawTragectory(Vector3 startPos, Vector3 velocity, Color color) {
        LineRenderer line = GetComponent<LineRenderer>();
        line.startColor = color;
        line.endColor = color;
        line.positionCount = 25;
        Vector2 curPos = startPos;
        Vector2 vel = velocity;

        for (int i = 0; i < 50; i++) {
            if (i % 2 == 0)
                line.SetPosition(i / 2, curPos);
            vel += Physics2D.gravity * 3 * Time.fixedDeltaTime;
            curPos += vel * Time.fixedDeltaTime;

        }

        line.materials[0].mainTextureScale = new Vector3(5, 1, 1);
    }

    void fireDirection(float angle, float speed) {
        GameObject arrow = Instantiate<GameObject>(arrowPrefab);
        arrow.transform.position = transform.position;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        arrow.GetComponent<Rigidbody2D>().velocity = arrow.transform.up * speed;
    }
}
