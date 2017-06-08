using UnityEngine;
using System.Collections;

public class Turrnet : MonoBehaviour {

    public string shootControl = "Shoot";

    public string horizontalControl;
    public string verticalControl;

    public float maxTurnAngle;

    public TurrentWeapon weapon;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 direction = new Vector3(Input.GetAxis(horizontalControl), Input.GetAxis(verticalControl)).normalized;
        if (direction == Vector3.zero)
            direction = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) - weapon.transform.position).normalized;
        Quaternion newRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        if (newRotation != weapon.transform.rotation) {
            weapon.transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, newRotation, maxTurnAngle);
            weapon.OnRotate();
        }

        if (weapon.hasAmmo()) {
            if (Input.GetAxis(shootControl) == 1) {
                weapon.triggerDown();
            }
            else
                weapon.triggerUp();
        }
    }
}