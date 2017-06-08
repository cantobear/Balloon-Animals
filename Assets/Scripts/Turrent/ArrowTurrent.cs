using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTurrent : TurrentWeapon {
    
    public GameObject arrowPrefab;

    public float firePower = 50;
    private float _charge = 0f;
    private float charge {
        get { return _charge; }
        set { _charge = value; updateTragectory(); }
    }
    public float chargeTime;
    private float chargeTimePassed;
    public float chargeSpeedExponent;

    public int maxArrowCount;
    private float _arrowCount;
    public float arrowCount {
        get { return _arrowCount; }
    }
    public float arrowRespawnTime;

    private LineRenderer line;

    public LayerMask mask;

    AudioSource audio;
    public AudioClip arrowSound;
    
    

    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
        line.materials[0].mainTextureScale = new Vector3(5, 1, 1);

        audio = GetComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void Update () {
        _arrowCount = Mathf.Min(arrowCount + Time.deltaTime / arrowRespawnTime, maxArrowCount);
    }

    public override void OnRotate() {
        updateTragectory();
    }

    public override bool hasAmmo() {
        return _arrowCount >= 1;
    }

    public override void triggerDown() {
        chargeTimePassed = Mathf.Min(chargeTimePassed + Time.deltaTime / chargeTime, 1);
        if (charge < 1) {
            charge = 1 - Mathf.Pow((-chargeTimePassed + 1), chargeSpeedExponent);
        }
    }

    public override void triggerUp() {
        if (charge == 1) {
            audio.Play();
            fireDirection(Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg - 85, charge * firePower);
            fireDirection(Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg - 90, charge * firePower);
            fireDirection(Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg - 95, charge * firePower);
            charge = chargeTimePassed = 0;
            --_arrowCount;
        }
        else if (charge > 0) {
            audio.Play();
            fireDirection(Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg - 90, charge * firePower);
            charge = chargeTimePassed = 0;
            --_arrowCount;
        }
    }

    private void updateTragectory() {
        if (charge == 1) {
            drawTragectory(transform.position, transform.up * charge * firePower, Color.yellow);
        }
        else
            drawTragectory(transform.position, transform.up * charge * firePower, Color.white);
    }

    void drawTragectory(Vector3 startPos, Vector3 velocity, Color color) {
        int linePerTick = 3;
        float maxTime = 3;
        int count = (int)Mathf.Floor(maxTime / Time.fixedDeltaTime / linePerTick);
        line.startColor = color;
        line.endColor = color;
        line.positionCount = count;
        Vector2 prevPos = startPos;
        Vector2 curPos = startPos;
        Vector2 vel = velocity;
        RaycastHit2D hit;

        for (int i = 0; i < count * linePerTick; i++) {
            if (i % linePerTick == 0) {
                hit = Physics2D.Linecast(prevPos, curPos, mask);
                if (hit == true) {
                    line.SetPosition(i / linePerTick, hit.point);
                    line.positionCount = (i / linePerTick) + 1;
                    break;
                }
                line.SetPosition(i / linePerTick, curPos);
                prevPos = curPos;
            }
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
