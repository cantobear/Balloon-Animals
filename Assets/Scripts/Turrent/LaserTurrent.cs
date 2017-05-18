using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurrent : TurrentWeapon {

    private LineRenderer laserLine;
    private LayerMask mask;
    private GameObject particleSystemObject;
    private ParticleSystem particleSystem;
    public int particlesPerUnit;
    private GameObject laserHit;

    // Use this for initialization
    void Start () {
        laserLine = GetComponent<LineRenderer>();
        laserLine.positionCount = 2;
        laserLine.startWidth = laserLine.endWidth = 1f;
        laserLine.materials[0].mainTextureScale = new Vector3(1, 1, 1);
        laserLine.SetPosition(0, transform.position);
        mask = ~(LayerMask.GetMask("Ignore") + LayerMask.GetMask("Wind") + LayerMask.GetMask("Turrent"));

        particleSystem = transform.GetComponentInChildren<ParticleSystem>();
        particleSystemObject = particleSystem.gameObject;

        laserHit = transform.GetComponentInChildren<LaserHit>().gameObject;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.up, 30, mask);
        float length = Vector3.Distance(transform.position, hit.point);
        particleSystemObject.transform.position = transform.position + transform.up * length / 2;

        laserLine.SetPosition(1, transform.position + transform.up * length);
        var shape = particleSystem.shape;
        shape.box = new Vector3(shape.box.x, length, 0);

        var emission = particleSystem.emission;
        emission.rateOverTime = length * particlesPerUnit;

        laserHit.transform.position = hit.point;
    }

    public override void triggerDown() {

    }

    public override void triggerUp() {

    }
}
