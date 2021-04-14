using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Vector3 cameraTarget;
    public Camera camera;
    public float targetSpeed;
    public float speedDV;
    private float speed;
    private bool trigger;

    // Start is called before the first frame update
    void Start()
    {
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger) {
        Debug.Log(cameraTarget);
            speed = Mathf.SmoothDamp(speed, targetSpeed, ref speedDV, 0.5f);
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, cameraTarget, speed * Time.deltaTime);
        }
        if(Vector3.Distance(camera.transform.position, cameraTarget) < 1f) trigger = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        trigger = true;
    }

}
