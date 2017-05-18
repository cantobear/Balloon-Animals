using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCountUI : MonoBehaviour {

    public Turrnet turrent;
    private Slider slide;

	// Use this for initialization
	void Start () {
        slide = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        //slide.value = turrent.arrowCount;
	}
}
