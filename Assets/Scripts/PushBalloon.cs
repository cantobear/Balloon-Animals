using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBalloon : MonoBehaviour {

    public float punchSpeed;

    void OnTriggerEnter(Collider coll) {
        if (coll.CompareTag("Balloon")) {
            coll.gameObject.GetComponentInParent<BalloonBehaviour>().onPunch(new Vector3(transform.position.x, coll.transform.position.y - 1), Mathf.Max(GetComponent<Rigidbody>().velocity.y + punchSpeed, punchSpeed));
        }
    }
}
