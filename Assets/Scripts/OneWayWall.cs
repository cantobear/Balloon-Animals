using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayWall : MonoBehaviour {

    public float distanceToTrigger;
    private Collider col;

	void Start () {
        //creating the trigger
        GameObject trigger = new GameObject();
        trigger.AddComponent<TriggerDetection>();
        trigger.tag = tag;
        trigger.layer = gameObject.layer;
        BoxCollider box = trigger.AddComponent<BoxCollider>();
        box.isTrigger = true;
        //setting transform
        trigger.transform.parent = transform;
        trigger.transform.position = transform.position;
        trigger.transform.localScale = Vector3.one;
        //calculating scale based off parent scale and distanceToTrigger
        Vector3 scale = trigger.transform.parent.lossyScale;
        scale.x = (scale.x - distanceToTrigger) / scale.x;
        scale.y = (scale.y - distanceToTrigger) / scale.y + 1;
        //setting collider
        box.center = new Vector3(box.center.x, 0.5f, box.center.z);
        box.size = scale;


        col = GetComponent<Collider>();
    }
	
	protected void enableCollision(Collider col) {
        Physics.IgnoreCollision(this.col, col, false);
    }

    protected void disableCollision(Collider col) {
        Physics.IgnoreCollision(this.col, col);
    }

    private class TriggerDetection : MonoBehaviour {
        OneWayWall parent;

        void Start() {
            parent = transform.parent.GetComponent<OneWayWall>();
        }

        void OnTriggerEnter(Collider col) {
            if (!col.CompareTag("Player"))
                parent.disableCollision(col);
        }

        void OnTriggerExit(Collider col) {
            parent.enableCollision(col);
        }
    }
}
