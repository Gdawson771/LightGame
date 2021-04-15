using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private GameObject player;
    public float targetSpeed;
    public float speedDV;
    public float catchUpDistance;
    public float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetSpeed = speed;
    }

    void FixedUpdate()
    {
        Vector3 target = new Vector3( player.transform.position.x, player.transform.position.y, transform.position.z);
        if(Vector3.Distance(transform.position, target) > catchUpDistance) {
            Debug.Log("Here 2");

            targetSpeed += 0.1f;
        } else {
            Debug.Log("Here 1");
            Debug.Log(targetSpeed);
            targetSpeed = speed;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, targetSpeed * Time.deltaTime);

    }
}
