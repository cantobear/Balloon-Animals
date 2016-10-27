using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour
{

   // Ray ray;
    //RaycastHit hit;
    public GameObject balloon;
    public Camera cam;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            {
            var mousePos = Input.mousePosition;
            mousePos.z = 2f;
            var position = cam.ScreenToWorldPoint(mousePos);
            position.z = 0;
            Instantiate(balloon, position, Quaternion.identity);
            }
    }
}
