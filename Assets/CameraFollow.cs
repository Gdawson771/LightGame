using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private GameObject player;
    public float targetSpeed;
    public float speedDV;
    public float catchUpDistance;
    private float speed;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {

        Vector3 target = new Vector3( player.transform.position.x, player.transform.position.y, transform.position.z);
        Debug.Log(Vector3.Distance(transform.position, target));
        if(Vector3.Distance(transform.position, target) > catchUpDistance) {
            Debug.Log("Here 1");
            speed += 0.1f;
        } else {
            Debug.Log("Here");
            if(speed != targetSpeed)
                speed -= 0.2f;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }
}
