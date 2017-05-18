using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {

    private Vector2 _windVector;
    public Vector2 speedRange;
    public Vector2 windVector {
        get { return _windVector + new Vector2(Random.Range(-speedRange.x, speedRange.x), Random.Range(-speedRange.y, speedRange.y)); }
        set { _windVector = value; }
    }
    public Vector2 getRandomWindVector(int seed) {
        Random.seed = seed;
        return _windVector + new Vector2(Random.Range(-speedRange.x, speedRange.x), Random.Range(-speedRange.y, speedRange.y));
    }

    public Vector2 windDirection {
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
        transform.position = Vector3.down * 20;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
