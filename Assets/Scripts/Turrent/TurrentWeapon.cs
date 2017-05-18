using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurrentWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual bool hasAmmo() {
        return true;
    }

    public abstract void triggerDown();

    public abstract void triggerUp();
}
