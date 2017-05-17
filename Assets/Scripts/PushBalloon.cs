using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBalloon : MonoBehaviour {

    public float punchSpeed;

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Balloon")) {
            coll.gameObject.GetComponentInParent<BalloonBehaviour>().onPunch(new Vector2(transform.position.x, coll.transform.position.y - 1), Mathf.Max(GetComponent<Rigidbody2D>().velocity.y + punchSpeed, punchSpeed));
        }
    }
}
