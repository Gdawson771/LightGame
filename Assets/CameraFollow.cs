using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform playerTransform;
    public float zindex;
    public float yOffset;
    public float xOffset;


    public bool followX = true;
    public bool followY = true;

    void Start()
    {

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {

        // here we store current camera position as a temporary position
        Vector3 temp = transform.position;

        // set cameras position to be equal to player
        temp.z = zindex;

        if (followY)
            temp.y = yOffset != null ? playerTransform.position.y + yOffset : playerTransform.position.y;

        if (followX)
            temp.x = xOffset != null ? playerTransform.position.x + xOffset : playerTransform.position.x;

        // set cameras temp position to current camera position
        transform.position = temp;
    }
}
