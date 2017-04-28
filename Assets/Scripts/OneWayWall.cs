using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayWall : MonoBehaviour {

    private Collider col;

	void Start () {
        transform.GetChild(0).gameObject.AddComponent<TriggerDetection>();
        col = GetComponent<Collider>();

    }
	
	protected void enableCollision(Collider col) {
        Physics.IgnoreCollision(this.col, col, false);
        Debug.Log("enter");
    }

    protected void disableCollision(Collider col) {
        Physics.IgnoreCollision(this.col, col);
        Debug.Log("exit");
    }

    private class TriggerDetection : MonoBehaviour {
        OneWayWall parent;

        void Start() {
            parent = transform.parent.GetComponent<OneWayWall>();
        }

        void OnTriggerEnter(Collider col) {
            parent.disableCollision(col);
        }

        void OnTriggerExit(Collider col) {
            parent.enableCollision(col);
        }
    }
}
