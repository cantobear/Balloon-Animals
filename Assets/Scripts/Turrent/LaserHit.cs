using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LaserHit : MonoBehaviour {

    public float HeatPerSec;
    public float HeatDropOffExponent;
    private Collider2D collider;

    void Start() {
        collider = GetComponent<Collider2D>();
    }

    private float distanceEffectiveness(Vector3 position) {
        return Mathf.Max(0, 1 - Mathf.Pow(Vector3.Distance(transform.position, position)/collider.bounds.extents.x, HeatDropOffExponent));
    }

    void OnTriggerStay2D(Collider2D coll) {
        BalloonBehaviour Balloon = coll.GetComponent<BalloonBehaviour>();
        if (Balloon) {
            Balloon.addHeat(HeatPerSec * distanceEffectiveness(coll.transform.position) * Time.fixedDeltaTime);
        }
    }
}
