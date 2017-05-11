using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {

    private Vector3 _windVector;
    public Vector3 speedRange;
    public Vector3 windVector {
        get { return _windVector + new Vector3(Random.Range(-speedRange.x, speedRange.x), Random.Range(-speedRange.y, speedRange.y), Random.Range(-speedRange.z, speedRange.z)); }
        set { _windVector = value; }
    }
    public Vector3 windDirection {
        get { return _windVector.normalized; }
        set { _windVector = value.normalized * _windVector.magnitude; }
    }
    public virtual float windSpeed {
        get { return _windVector.magnitude; }
        set { _windVector = _windVector.normalized * value; }
    }

    public void destroy(float time = 0) {
        StartCoroutine("delayDestroy", time);
    }

    private IEnumerator delayDestroy(float time) {
        yield return new WaitForSeconds(time);
        transform.position = Vector3.down * 1000;
        Destroy(gameObject, 1f);
    }
}
