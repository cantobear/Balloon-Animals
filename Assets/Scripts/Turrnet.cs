using UnityEngine;
using System.Collections;

public class Turrnet : MonoBehaviour {

    public string shootControl = "Shoot";
    public float firePower = 50;
    GameObject arrowPrefab;
    private float charge = 0f;
    public float chargeTime;
    private float chargeTimePassed;
    Transform turrentDirection;

    public int maxArrowCount;
    private float _arrowCount;
    public float arrowCount {
        get { return _arrowCount; }
    }
    public float arrowRespawnTime;

    public string horizontalControl;
    public string verticalControl;

    // Use this for initialization
    void Start () {
        arrowPrefab = Resources.Load<GameObject>("Arrow");
        turrentDirection = new GameObject("TurrentDirection").transform;
        turrentDirection.position = transform.position;
        turrentDirection.SetParent(transform);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = new Vector3(Input.GetAxis(horizontalControl), Input.GetAxis(verticalControl)).normalized;
        if (direction == Vector3.zero)
            direction = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, turrentDirection.position.z) - turrentDirection.position).normalized;
        turrentDirection.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);

        _arrowCount = Mathf.Min(arrowCount + Time.deltaTime / arrowRespawnTime, maxArrowCount);

        if (_arrowCount >= 1) {
            if (Input.GetAxis(shootControl) == 1) {
                chargeTimePassed = Mathf.Min(chargeTimePassed + Time.deltaTime / chargeTime, 1);
                charge = Mathf.Pow(chargeTimePassed, 0.4f);
            }
            else if (charge == 1) {
                fireDirection(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 85, charge * firePower);
                fireDirection(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90, charge * firePower);
                fireDirection(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 95, charge * firePower);
                charge = chargeTimePassed = 0;
                --_arrowCount;
            }
            else if (charge > 0) {
                fireDirection(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90, charge * firePower);
                charge = chargeTimePassed = 0;
                --_arrowCount;
            }
            if (charge == 1) {
                drawTragectory(turrentDirection.position, turrentDirection.up * charge * firePower, Color.yellow);
            } else
                drawTragectory(turrentDirection.position, turrentDirection.up * charge * firePower, Color.white);
        }
    }

    void drawTragectory(Vector3 startPos, Vector3 velocity, Color color) {
        LineRenderer line = GetComponent<LineRenderer>();
        line.startColor = color;
        line.endColor = color;
        line.positionCount = 25;
        Vector2 curPos = startPos;
        Vector2 vel = velocity;

        for(int i = 0; i < 50; i++) {
            if (i%2 == 0)
                line.SetPosition(i/2, curPos);
            vel += Physics2D.gravity * 3 * Time.fixedDeltaTime;
            curPos += vel * Time.fixedDeltaTime;

        }

        line.materials[0].mainTextureScale = new Vector3(5, 1, 1);
    }

    void fireDirection(float angle, float speed) {
        GameObject arrow = Instantiate<GameObject>(arrowPrefab);
        arrow.transform.position = turrentDirection.position;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        arrow.GetComponent<Rigidbody2D>().velocity = arrow.transform.up * speed;
    }
}
