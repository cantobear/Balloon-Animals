using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayWall : MonoBehaviour {

    public float distanceToTrigger;
    private Collider2D col;

	void Start () {
        //creating the trigger
        GameObject trigger = new GameObject();
        trigger.AddComponent<TriggerDetection>();
        trigger.tag = tag;
        trigger.layer = gameObject.layer;
        BoxCollider2D box = trigger.AddComponent<BoxCollider2D>();
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
        box.offset = new Vector2(box.offset.x, 0.5f);
        box.size = scale;


        col = GetComponent<Collider2D>();
    }
	
	protected void enableCollision(Collider2D col) {
        Physics2D.IgnoreCollision(this.col, col, false);
    }

    protected void disableCollision(Collider2D col) {
        Physics2D.IgnoreCollision(this.col, col);
    }

    private class TriggerDetection : MonoBehaviour {
        OneWayWall parent;

        void Start() {
            parent = transform.parent.GetComponent<OneWayWall>();
        }

        void OnTriggerEnter2D(Collider2D col) {
            if (!col.CompareTag("Player"))
                parent.disableCollision(col);
        }

        void OnTriggerExit2D(Collider2D col) {
            parent.enableCollision(col);
        }
    }
}
