using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {

    public Vector3 _windVector;
    public Vector3 windVector {
        get { return _windVector; }
        set { _windVector = value; }
    }
    public Vector3 windDirection {
        get { return _windVector.normalized; }
    }
    public float windSpeed {
        get { return _windVector.magnitude; }
    }

    public void setWindDirection(Vector3 direction) {
        _windVector = direction.normalized * _windVector.magnitude;
    }

    public void setWindSpeed(float speed) {
        _windVector = _windVector.normalized * speed;
    }

    public void destroyAfter(float time) {
        StartCoroutine("destroy", time);
    }

    private IEnumerator destroy(float time) {
        yield return new WaitForSeconds(time);
        transform.position = Vector3.down * 1000;
        Destroy(gameObject, 1f);
    }
}
